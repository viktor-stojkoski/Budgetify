output "storage_account_connection_string" {
  value = azurerm_storage_account.sa.primary_connection_string
}
