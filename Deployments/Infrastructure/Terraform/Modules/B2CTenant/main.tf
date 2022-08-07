resource "azurerm_aadb2c_directory" "tenant" {
  data_residency_location = "Europe"
  domain_name             = lower("${replace(var.tenant_display_name, "/\\s+/", "")}.onmicrosoft.com")
  resource_group_name     = var.resource_group_name
  sku_name                = "PremiumP1"
  tags                    = var.tags
}
