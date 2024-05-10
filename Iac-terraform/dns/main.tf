terraform {
  backend "s3" {
    bucket         = "stack-infraascode"
    key            = "dns.tfstate"
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


resource "aws_service_discovery_private_dns_namespace" "stack_dns_discovery" {
  name        = var.stack_private_dns_namespace
  description = "stack dns discovery"
  vpc         = data.terraform_remote_state.vpc.outputs.stack_vpc_id
}