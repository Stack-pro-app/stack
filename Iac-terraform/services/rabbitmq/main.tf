terraform {
  backend "s3" {
    bucket         = "stack-infraascode"
    key            = "rabbitmq.tfstate"
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

resource "aws_iam_policy" "rabbitmq_task_role_policy" {
  name        = "rabbitmq_task_role_policy"
  description = "rabbitmq task role policy"

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

resource "aws_iam_role" "rabbitmq_task_role" {
  name = "rabbitmq_task_role"

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
  role       = aws_iam_role.rabbitmq_task_role.name
  policy_arn = aws_iam_policy.rabbitmq_task_role_policy.arn
}

resource "aws_ecs_task_definition" "rabbitmq_td" {
  family                   = "rabbitmq_td"
  network_mode             = "awsvpc"
  requires_compatibilities = ["FARGATE"]
  cpu                      = "256"
  memory                   = "512"
  execution_role_arn       = aws_iam_role.rabbitmq_task_role.arn

  container_definitions = jsonencode(
    [
      {
        cpu : 256,
        image : "rabbitmq:3-management-alpine",
        memory : 512,
        name : "rabbitmq",
        networkMode : "awsvpc",
        environment = [
        {
          name  = "RABBITMQ_DEFAULT_USER"
          value = "user"
        },
        {
          name  = "RABBITMQ_DEFAULT_PASS"
          value = "password"
        }
      ],
        portMappings : [
          {
            containerPort : 15672,
            hostPort : 15672
          },
          {
            containerPort : 5672,
            hostPort : 5672
          }
        ],
        logConfiguration : {
          logDriver : "awslogs",
          options : {
            awslogs-group : "/ecs/stack_log_group",
            awslogs-region : "us-east-1",
            awslogs-stream-prefix : "rabbitmq"
          }
        }
      }
    ]
  )
}
resource "aws_ecs_service" "rabbitmq_td_service" {
  name            = "rabbitmq_td_service"
  cluster         = data.terraform_remote_state.ecs_cluster.outputs.stack_ecs_cluster_id
  task_definition = aws_ecs_task_definition.rabbitmq_td.arn
  desired_count   = "1"
  launch_type     = "FARGATE"

  network_configuration {
    security_groups = ["${aws_security_group.ecs_mq_tasks_sg.id}"]
    subnets         = ["${data.terraform_remote_state.vpc.outputs.stack_private_subnets_ids[0]}"]
  }
  load_balancer {
    target_group_arn = aws_alb_target_group.rabbitmq_tg.id
    container_name   = "rabbitmq"
    container_port   = "15672"
  }
  service_registries {
    registry_arn = aws_service_discovery_service.rabbitmq_service.arn
  }
}
resource "aws_security_group" "ecs_mq_tasks_sg" {
  name        = "ecs_mq_tasks_sg"
  vpc_id      = data.terraform_remote_state.vpc.outputs.stack_vpc_id

  ingress {
    protocol        = "tcp"
    from_port       = "5672"
    to_port         = "5672"
    cidr_blocks = ["10.0.0.0/16"]
  }
  ingress {
    protocol        = "tcp"
    from_port       = "15672"
    to_port         = "15672"
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

resource "aws_alb_target_group" "rabbitmq_tg" {
  name        = "rqbbitmq-tg"
  port        = "15672"
  protocol    = "HTTP"
  vpc_id      = data.terraform_remote_state.vpc.outputs.stack_vpc_id
  target_type = "ip"
  health_check {
    path = "/"
  }
}

resource "aws_alb_listener" "rabbitmq_tg_listener" {
  load_balancer_arn = data.terraform_remote_state.alb.outputs.stack_alb_id
  port              = "15672"
  protocol          = "HTTP"

  default_action {
    target_group_arn = aws_alb_target_group.rabbitmq_tg.id
    type             = "forward"
  }
}

resource "aws_service_discovery_service" "rabbitmq_service" {
  name = var.rabbitmq_service_namespace

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



