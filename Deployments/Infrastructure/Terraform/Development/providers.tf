provider "azurerm" {
  features {}
  subscription_id = var.azure_subscription
}

provider "azuread" {}

provider "random" {}
