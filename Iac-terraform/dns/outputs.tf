output "stack_dns_discovery_id" {
  description = "stack service discovery id"
  value       = aws_service_discovery_private_dns_namespace.stack_dns_discovery.id
}

output "stack_private_dns_namespace" {
  description = "stack service discovery id"
  value       = var.stack_private_dns_namespace
}