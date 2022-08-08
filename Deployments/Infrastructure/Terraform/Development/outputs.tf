output "graph_client_id" {
  value       = module.azure_ad_b2c.graph_client_id
  description = "Microsoft Graph API Client ID"
}

output "graph_secret" {
  value       = module.azure_ad_b2c.graph_secret
  description = "Microsoft Graph API Secret"
  sensitive   = true
}

output "b2c_tenant_id" {
  value       = module.b2c_tenant.tenant_id
  description = "B2C Tenant ID"
}

output "tenant_id" {
  value       = data.azurerm_client_config.current.tenant_id
  description = "Current tenant ID"
}

output "api_connector_password" {
  description = "API Connector Password"
  value       = random_password.api_connector_password.result
  sensitive   = true
}
