terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }
  backend "s3" {
    bucket         = "stack-infraascode"
    key            = "alb.tfstate"
    region         = "us-east-1"
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

resource "aws_lb" "stack-alb" {
    load_balancer_type = "application"
    subnets = data.terraform_remote_state.vpc.outputs.stack_public_subnets_ids
    security_groups = ["${aws_security_group.stack_alb_sg.id}"]
}

resource "aws_security_group" "stack_alb_sg" {
  description = "controls access to the ALB"
  vpc_id      = data.terraform_remote_state.vpc.outputs.stack_vpc_id

  ingress {
    protocol    = "tcp"
    from_port   = 15672
    to_port     = 15672
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    protocol    = "tcp"
    from_port   = 443
    to_port     = 443
    cidr_blocks = ["0.0.0.0/0"]
  }
  
  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}