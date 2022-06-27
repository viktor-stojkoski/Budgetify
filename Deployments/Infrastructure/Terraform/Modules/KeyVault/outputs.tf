output "secret_name" {
  description = "A mapping of secret names and IDs"
  value       = { for k, v in azurerm_key_vault_secret.secrets : v.name => v.id }
}
