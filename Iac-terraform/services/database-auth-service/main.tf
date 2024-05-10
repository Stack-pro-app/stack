terraform {
  backend "s3" {
    bucket         = "stack-infraascode"
    key            = "db4-service.tfstate"
    region         = "us-east-1"
  }
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }
}

provider "aws" {
  region  = "us-east-1"
}

data "terraform_remote_state" "vpc" {
  backend = "s3"
  config = {
    bucket = "stack-infraascode"
    key    = "vpc.tfstate"
    region = "us-east-1"
  }
}

data "terraform_remote_state" "dns" {
  backend = "s3"
  config = {
    bucket = "stack-infraascode"
    key    = "dns.tfstate"
    region = "us-east-1"
  }
}


data "terraform_remote_state" "ecs_cluster" {
  backend = "s3"
  config = {
    bucket = "stack-infraascode"
    key    = "ecs.tfstate"
    region = "us-east-1"
  }
}



resource "aws_iam_policy" "db4_task_role_policy" {
  name        = "db4_task_role_policy"
  description = "db4 task role policy"

  policy = jsonencode(
    {
      Version = "2012-10-17"
      Statement = [
        {
          Effect : "Allow",
          Action : [
            "logs:CreateLogStream",
            "logs:PutLogEvents"
          ],
          Resource : "*"
        }
      ]
    }
  )
}

resource "aws_iam_role" "db4_task_role" {
  name = "db4_task_role"

  assume_role_policy = jsonencode(
    {
      Version = "2012-10-17",
      Statement = [
        {
          Effect : "Allow",
          Principal : {
            Service : "ecs-tasks.amazonaws.com"
          },
          Action : [
            "sts:AssumeRole"
          ]
        }
      ]
    }
  )
}
resource "aws_iam_role_policy_attachment" "role-policy-attach" {
  role       = aws_iam_role.db4_task_role.name
  policy_arn = aws_iam_policy.db4_task_role_policy.arn
}


resource "aws_ecs_task_definition" "db4_td" {
  family                   = "db4_td"
  network_mode             = "awsvpc"
  requires_compatibilities = ["FARGATE"]
  cpu                      = "256"
  memory                   = "2048"
  execution_role_arn       = aws_iam_role.db4_task_role.arn

  container_definitions = jsonencode(
    [
      {
        cpu : 256,
        image : "mcr.microsoft.com/mssql/server:2022-latest",
        memory : 2048,
        name : "db2",
        networkMode : "awsvpc",
        environment = [
        {
          name  = "SA_PASSWORD"
          value = "password@12345#"
        },
        {
          name  = "ACCEPT_EULA"
          value = "Y"
        }
      ],
        portMappings : [
          {
            containerPort : 1433,
            hostPort : 1433
          }
        ],
        logConfiguration : {
          logDriver : "awslogs",
          options : {
            awslogs-group : "/ecs/stack_log_group",
            awslogs-region : "us-east-1",
            awslogs-stream-prefix : "db4"
          }
        }
      }
    ]
  )
}
resource "aws_ecs_service" "db4_td_service" {
  name            = "db4_td_service"
  cluster         = data.terraform_remote_state.ecs_cluster.outputs.stack_ecs_cluster_id
  task_definition = aws_ecs_task_definition.db4_td.arn
  desired_count   = "1"
  launch_type     = "FARGATE"

  network_configuration {
    security_groups = ["${aws_security_group.ecs_db4_tasks_sg.id}"]
    subnets         = ["${data.terraform_remote_state.vpc.outputs.stack_private_subnets_ids[0]}"]
  }

  service_registries {
    registry_arn = aws_service_discovery_service.db4_service.arn
  }
}
resource "aws_security_group" "ecs_db4_tasks_sg" {
  name        = "ecs_db4_tasks_sg"
  vpc_id      = data.terraform_remote_state.vpc.outputs.stack_vpc_id

  ingress {
    protocol        = "tcp"
    from_port       = "1433"
    to_port         = "1433"
    cidr_blocks = ["10.0.0.0/16"]
  }

  ingress {
    protocol  = -1
    from_port = 0
    to_port   = 0
    self      = true
  }

  egress {
    protocol    = "-1"
    from_port   = 0
    to_port     = 0
    cidr_blocks = ["0.0.0.0/0"]
  }
}


resource "aws_service_discovery_service" "db4_service" {
  name = var.db4_service_namespace

  dns_config {
    namespace_id = data.terraform_remote_state.dns.outputs.stack_dns_discovery_id

    dns_records {
      ttl  = 10
      type = "A"
    }

    routing_policy = "MULTIVALUE"
  }

  health_check_custom_config {
    failure_threshold = 2
  }
}



