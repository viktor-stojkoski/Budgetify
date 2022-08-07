data "azurerm_client_config" "current" {}

resource "azurerm_key_vault" "kv" {
  name                        = var.key_vault_name
  resource_group_name         = var.resource_group_name
  location                    = var.location
  tenant_id                   = data.azurerm_client_config.current.tenant_id
  enabled_for_disk_encryption = true
  soft_delete_retention_days  = 7
  purge_protection_enabled    = false
  sku_name                    = "standard"
  tags                        = var.tags
}

resource "azurerm_key_vault_access_policy" "current_principal" {
  key_vault_id = azurerm_key_vault.kv.id
  tenant_id    = data.azurerm_client_config.current.tenant_id
  object_id    = data.azurerm_client_config.current.object_id

  key_permissions         = local.all_key_permissions
  secret_permissions      = local.all_secret_permissions
  certificate_permissions = local.all_certificate_permissions
}

resource "azurerm_key_vault_access_policy" "viktor_stojkoski" {
  key_vault_id = azurerm_key_vault.kv.id
  tenant_id    = data.azurerm_client_config.current.tenant_id
  object_id    = local.viktor_stojkoski_object_id

  key_permissions         = local.all_key_permissions
  secret_permissions      = local.all_secret_permissions
  certificate_permissions = local.all_certificate_permissions
}

resource "azurerm_key_vault_secret" "secrets" {
  depends_on = [
    azurerm_key_vault_access_policy.current_principal
  ]

  for_each     = var.secrets
  name         = each.key
  value        = each.value
  key_vault_id = azurerm_key_vault.kv.id
}
