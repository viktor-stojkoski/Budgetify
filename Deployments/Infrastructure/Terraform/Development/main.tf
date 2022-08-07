terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
    }

    azuread = {
      source = "hashicorp/azuread"
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
  location             = var.location
  container_name       = lower(var.application_name)
  tags                 = merge(var.tags, local.tags)
}

module "b2c_tenant" {
  source              = "../Modules/B2CTenant"
  tenant_display_name = local.b2c_tenant_display_name
  resource_group_name = module.resource_group.resource_group_name
}

module "azure_ad_b2c" {
  source                         = "../Modules/B2CResources"
  tenant_id                      = module.b2c_tenant.tenant_id
  app_registration_display_name  = "${var.application_name} Angular"
  app_registration_redirect_uris = ["http://localhost:4200/"]
}

module "key_vault" {
  source              = "../Modules/KeyVault"
  key_vault_name      = local.key_vault_name
  resource_group_name = module.resource_group.resource_group_name
  location            = var.location
  tags                = merge(var.tags, local.tags)
  secrets = {
    "graphApi"                       = module.azure_ad_b2c.graph_secret
    "storageAccountConnectionString" = module.storage_account.storage_account_connection_string
    "apiConnectorPassword"           = local.apiConnectorPassword
  }
}
