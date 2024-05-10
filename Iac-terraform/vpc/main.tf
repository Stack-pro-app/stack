terraform {
  
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }
  backend "s3" {
    bucket         = "stack-infraascode"
    key            = "vpc.tfstate"
    region         = "us-east-1"
  }
}


provider "aws" {
  region  = "us-east-1"
}

resource "aws_cloudwatch_log_group" "stack_log_group" {
  name = "/ecs/stack_log_group"
}

module "vpc" {
  source = "terraform-aws-modules/vpc/aws"

  name = "stack-vpc"
  cidr = "10.0.0.0/16"

  azs             = ["us-east-1a", "us-east-1b"]
  private_subnets = ["10.0.1.0/24", "10.0.2.0/24"]
  public_subnets  = ["10.0.101.0/24", "10.0.102.0/24"]

  enable_dns_hostnames = true
  enable_dns_support   = true

  enable_nat_gateway = true
  single_nat_gateway = true

  tags = {
    group       = "stack"
    Environment = "dev"
  }
}