output "storage_account_connection_string" {
  description = "Storage account connection string"
  value       = azurerm_storage_account.sa.primary_connection_string
}

output "storage_account_name" {
  description = "Storage account name"
  value       = azurerm_storage_account.sa.name
}

output "storage_container" {
  description = "Storage container"
  value       = azurerm_storage_container.container
}
