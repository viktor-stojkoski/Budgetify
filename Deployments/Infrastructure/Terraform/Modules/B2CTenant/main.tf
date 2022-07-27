resource "azurerm_aadb2c_directory" "tenant" {
  country_code            = "ML" # Macedonia
  data_residency_location = "Europe"
  display_name            = var.tenant_display_name
  domain_name             = lower("${var.tenant_display_name}.onmicrosoft.com")
  resource_group_name     = var.resource_group_name
  sku_name                = "PremiumP1"
  tags                    = var.tags
}
