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
    # key                  = "evironments/dev/terraform.tfstate"
  }
}

provider "azurerm" {
  features {}
  subscription_id = var.azure_subscription
}

provider "azuread" {}

module "resource_group" {
  source   = "../Modules/ResourceGroup"
  name     = "rg-budgetify-tf" # TODO: Change from locals
  location = var.location
  tags     = merge(var.tags, local.tags)
}

module "storage_account" {
  source               = "../Modules/StorageAccount"
  resource_group_name  = module.resource_group.resource_group_name
  storage_account_name = "stbudgetifydevtest" # TODO: Change from locals
  location             = var.location
  container_name       = "budgetify"
  tags                 = merge(var.tags, local.tags)
}

module "b2c_tenant" {
  source              = "../Modules/B2CTenant"
  tenant_display_name = "BudgetifyTF" # TODO: Change after testing
  resource_group_name = module.resource_group.resource_group_name
  tags                = merge(var.tags, local.tags)
}

module "azure_ad_b2c" {
  source                         = "../Modules/B2CResources"
  tenant_id                      = module.b2c_tenant.tenant_id
  app_registration_display_name  = "BudgetifyTF Angular"
  app_registration_redirect_uris = ["http://localhost:4200/"]
}

module "key_vault" {
  source              = "../Modules/KeyVault"
  key_vault_name      = local.key_vault_name
  resource_group_name = module.resource_group.resource_group_name
  location            = var.location
  tenant_id           = var.tenant_id
  object_id           = var.object_id
  tags                = merge(var.tags, local.tags)
  secrets = {
    "graph-secret-test" = module.azure_ad_b2c.graph_secret # TODO: Revert test
  }
}
