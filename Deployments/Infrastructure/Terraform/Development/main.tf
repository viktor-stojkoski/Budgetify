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
  name     = "rg-budgetify-aaaa"
  location = var.location
}

module "storage_account" {
  source              = "../Modules/StorageAccount"
  resource_group_name = module.resource_group.name
  name                = "st-budgetify-dev"
  location            = var.location
  container_name      = "budgetify"
}
