terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
    }

    azuread = {
      source = "hashicorp/azuread"
    }

    random = {
      source = "hashicorp/random"
    }
  }

  backend "azurerm" {
    # resource_group_name  = "rg-budgetify-tfstate"
    # storage_account_name = "sabudgetifytfstate"
    # container_name       = "terraform-state"
    # key                  = "dev.terraform.tfstate"
  }
}

module "resource_group" {
  source   = "../Modules/ResourceGroup"
  name     = local.resource_group_name
  location = var.location
  tags     = merge(var.tags, local.tags)
}

module "storage_account" {
  source               = "../Modules/StorageAccount"
  resource_group_name  = module.resource_group.resource_group_name
  storage_account_name = local.storage_account_name
  account_kind         = "BlockBlobStorage"
  location             = var.location
  container_name       = lower(var.application_name)
  tags                 = merge(var.tags, local.tags)
}

module "storage_account" {
  source               = "../Modules/StorageAccount"
  resource_group_name  = module.resource_group.resource_group_name
  storage_account_name = "test-budgetify"
  location             = var.location
  container_name       = lower(var.application_name)
  tags                 = merge(var.tags, local.tags)
}

module "key_vault" {
  source              = "../Modules/KeyVault"
  key_vault_name      = local.key_vault_name
  resource_group_name = module.resource_group.resource_group_name
  location            = var.location
  tags                = merge(var.tags, local.tags)
  secrets = {
    "storageAccountConnectionString" = module.storage_account.storage_account_connection_string
  }
}

module "cognitive_service_account" {
  source                 = "../Modules/CognitiveService"
  cognitive_service_name = local.cognitive_service_name
  location               = var.location
  resource_group_name    = module.resource_group.resource_group_name
  tags                   = merge(var.tags, local.tags)
}
