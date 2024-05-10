output "stack_vpc_id" {
  description = "stack subnet private 1"
  value       = module.vpc.vpc_id
}

output "stack_private_subnets_ids" {
  description = "stack private subnets ids"
  value       = module.vpc.private_subnets
}

output "stack_public_subnets_ids" {
  description = "fgms public subnets ids"
  value       = module.vpc.public_subnets
}