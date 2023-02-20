resource "azurerm_cognitive_account" "ca" {
  name                          = var.cognitive_service_name
  location                      = var.location
  resource_group_name           = var.resource_group_name
  kind                          = "FormRecognizer"
  sku_name                      = "F0"
  public_network_access_enabled = true
  tags                          = var.tags
  # storage {
  #   storage_account_id = var.storage_account_id
  # }
}
