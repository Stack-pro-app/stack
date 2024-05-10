terraform {
  backend "s3" {
    bucket         = "stack-infraascode"
    key            = "ecs.tfstate"
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


resource "aws_ecs_cluster" "stack_ecs_cluster" {
  name = "stack_ecs_cluster"
}