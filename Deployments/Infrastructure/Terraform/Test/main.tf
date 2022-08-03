terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
    }
  }

  backend "azurerm" {
    # resource_group_name  = "rg-budgetify-tfstate"
    # storage_account_name = "sabudgetifytfstate"
    # container_name       = "terraform-state"
    # key                  = "dev.terraform.tfstate"
  }
}

provider "azurerm" {
  features {}
}

module "resource_group" {
  source   = "../Modules/ResourceGroup"
  name     = "testrg"
  location = "West Europe"
}

module "storage_account" {
  source               = "../Modules/StorageAccount"
  resource_group_name  = module.resource_group.resource_group_name
  storage_account_name = "budgetifyytestst"
  location             = "West Europe"
  container_name       = "test"
}

output "resource_group_name" {
  value = module.resource_group.resource_group_name
}

output "storage_account_name" {
  value       = module.storage_account.storage_account_name
  sensitive   = true
  description = "Test"
}
