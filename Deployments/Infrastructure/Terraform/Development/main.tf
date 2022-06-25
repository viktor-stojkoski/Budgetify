terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
    }
  }

  backend "azurerm" {
    # resource_group_name  = "rg-budgetify-tfstate"
    # storage_account_name = "stbudgetifytfstate"
    # container_name       = "terraform-state"
    # key                  = "evironments/dev/terraform.tfstate"
  }
}

provider "azurerm" {
  features {}
}

module "resource_group" {
  source   = "../Modules/ResourceGroup"
  name     = "rg-budgetify-tf" # TODO: Change from locals
  location = var.location
  tags     = merge(var.tags, local.tags)
}

module "storage_account" {
  source               = "../Modules/StorageAccount"
  resource_group_name  = module.resource_group.resource_group_name
  storage_account_name = "stbudgetifydevtest" # TOD: Change from locals
  location             = var.location
  container_name       = "budgetify"
  tags                 = merge(var.tags, local.tags)
}
