output "graph_client_id" {
  value       = module.azure_ad_b2c.graph_client_id
  description = "Microsoft Graph API Client ID"
}

output "graph_client_secret" {
  value       = module.azure_ad_b2c.graph_secret
  description = "Microsoft Graph API Secret"
  sensitive   = true
}

output "b2c_tenant_domain_name" {
  value       = module.b2c_tenant.tenant_domain_name
  description = "B2C Tenant ID"
}

output "api_connector_password" {
  description = "API Connector Password"
  value       = random_password.api_connector.result
  sensitive   = true
}
