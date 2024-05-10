output "stack_alb_sg_id" {
  description = "stack private subnets ids"
  value       = aws_security_group.stack_alb_sg.id
}

output "stack_alb_id" {
  description = "stack public alb"
  value       = aws_lb.stack-alb.id
}

output "stack_alb_dns_name" {
  description = "stack public alb dns name"
  value       = aws_lb.stack-alb.dns_name
}

output "stack_alb_zone_id" {
  description = "stack public alb zone id"
  value       = aws_lb.stack-alb.zone_id
}