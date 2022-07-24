terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
    }

    azuread = {
      source = "hashicorp/azuread"
    }

    null = {
      source = "hashicorp/null"
    }
  }

  backend "azurerm" {
    # resource_group_name  = "rg-budgetify-tfstate"
    # storage_account_name = "stbudgetifytfstate"
    # container_name       = "terraform-state"
    # key                  = "b2c.terraform.tfstate"
  }
}

provider "azurerm" {
  features {}
}

module "azure_ad_b2c" {
  source                         = "../Modules/AzureAdB2C"
  tenant_display_name            = "BudgetifyTF" # TODO: Change after testing
  tenant_domain_name             = "budgetifytf.onmicrosoft.com"
  resource_group_name            = var.resource_group_name
  tags                           = merge(var.tags, local.tags)
  app_registration_display_name  = "BudgetifyTF Angular"
  app_registration_redirect_uris = ["http://localhost:4200/"]
}
