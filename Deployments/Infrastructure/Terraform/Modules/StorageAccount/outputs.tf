output "storage_account_connection_string" {
  value = azurerm_storage_account.sa.primary_connection_string
}

output "storage_account_name" {
  value = azurerm_storage_account.sa.name
}

output "storage_container" {
  value = azurerm_storage_container.container
}
