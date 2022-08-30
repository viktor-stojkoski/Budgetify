terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
    }
  }
}

provider "azurerm" {
  features {}
}

module "resource_group" {
  source   = "../Modules/ResourceGroup"
  name     = local.resource_group_name
  location = var.location
  tags     = var.tags
}

module "storage_account" {
  source               = "../Modules/StorageAccount"
  resource_group_name  = module.resource_group.resource_group_name
  location             = var.location
  storage_account_name = local.storage_account_name
  account_tier         = "Standard"
  account_kind         = "StorageV2"
  container_name       = "terraform-state"
  tags                 = var.tags
}
