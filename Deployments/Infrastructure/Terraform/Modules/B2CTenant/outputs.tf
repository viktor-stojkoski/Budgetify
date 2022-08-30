output "tenant_id" {
  value       = azurerm_aadb2c_directory.tenant.tenant_id
  description = "B2C Tenant ID"
}

output "tenant_domain_name" {
  value       = azurerm_aadb2c_directory.tenant.domain_name
  description = "B2C Tenant domain name"
}
