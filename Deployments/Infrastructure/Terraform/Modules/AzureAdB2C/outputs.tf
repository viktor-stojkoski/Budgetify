output "graph_client_id" {
  description = "Microsoft Graph Application Client ID"
  value       = azuread_application.microsoft_graph.application_id
}

output "graph_secret" {
  description = "Microsoft Graph API secret"
  value       = azuread_application_password.graph_secret.value
  sensitive   = true
}

output "b2c_tenant_id" {
  description = "B2C Tenant ID"
  value       = azurerm_aadb2c_directory.tenant.tenant_id
}
