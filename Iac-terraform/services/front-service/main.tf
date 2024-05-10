terraform {
  backend "s3" {
    bucket         = "stack-infraascode"
    key            = "front-service.tfstate"
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

data "terraform_remote_state" "alb" {
  backend = "s3"
  config = {
    bucket = "stack-infraascode"
    key    = "alb.tfstate"
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

resource "aws_iam_policy" "front_service_task_role_policy" {
  name        = "front_service_task_role_policy"
  description = "front service task role policy"

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

resource "aws_iam_role" "front_service_task_role" {
  name = "front_service_task_role"

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
  role       = aws_iam_role.front_service_task_role.name
  policy_arn = aws_iam_policy.front_service_task_role_policy.arn
}

resource "aws_ecs_task_definition" "front_service_td" {
  family                   = "front_service_td"
  network_mode             = "awsvpc"
  requires_compatibilities = ["FARGATE"]
  cpu                      = "256"
  memory                   = "512"
  execution_role_arn       = aws_iam_role.front_service_task_role.arn

  container_definitions = jsonencode(
    [
      {
        cpu : 256,
        image : "oussamazaoui872/slack-front:latest",
        memory : 512,
        name : "front",
        networkMode : "awsvpc",
        portMappings : [
          {
            containerPort : 80,
            hostPort : 80
          }
        ],
        logConfiguration : {
          logDriver : "awslogs",
          options : {
            awslogs-group : "/ecs/stack_log_group",
            awslogs-region : "us-east-1",
            awslogs-stream-prefix : "front"
          }
        }
      }
    ]
  )
}
resource "aws_ecs_service" "front_td_service" {
  name            = "front_td_service"
  cluster         = data.terraform_remote_state.ecs_cluster.outputs.stack_ecs_cluster_id
  task_definition = aws_ecs_task_definition.front_service_td.arn
  desired_count   = "1"
  launch_type     = "FARGATE"

  network_configuration {
    security_groups = ["${aws_security_group.ecs_tasks_sg.id}"]
    subnets         = ["${data.terraform_remote_state.vpc.outputs.stack_private_subnets_ids[0]}"]
  }

  load_balancer {
    target_group_arn = aws_alb_target_group.front_tg.id
    container_name   = "front"
    container_port   = 80
  }

  service_registries {
    registry_arn = aws_service_discovery_service.front_service.arn
  }
}
resource "aws_security_group" "ecs_tasks_sg" {
  name        = "ecs_tasks_sg"
  description = "allow inbound access from the ALB only"
  vpc_id      = data.terraform_remote_state.vpc.outputs.stack_vpc_id

  ingress {
    protocol        = "tcp"
    from_port       = "80"
    to_port         = "80"
    security_groups = ["${data.terraform_remote_state.alb.outputs.stack_alb_sg_id}"]
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


resource "aws_alb_target_group" "front_tg" {
  name        = "front-tg"
  port        = 80
  protocol    = "HTTP"
  vpc_id      = data.terraform_remote_state.vpc.outputs.stack_vpc_id
  target_type = "ip"
  health_check {
    path = "/"
  }
}

resource "aws_alb_listener" "front_tg_listener" {
  load_balancer_arn = data.terraform_remote_state.alb.outputs.stack_alb_id
  port              = "80"
  protocol          = "HTTP"

  default_action {
    target_group_arn = aws_alb_target_group.front_tg.id
    type             = "forward"
  }
}


resource "aws_service_discovery_service" "front_service" {
  name = var.front_service_namespace

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



