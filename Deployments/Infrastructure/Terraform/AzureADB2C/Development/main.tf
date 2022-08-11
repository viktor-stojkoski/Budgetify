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
    # key                  = "b2c/dev.terraform.tfstate"
  }
}

module "azure_ad_b2c" {
  source = "../Modules/B2CResources"
  # tenant_id                      = var.tenant_id
  app_registration_display_name  = "${var.application_name} Angular"
  app_registration_redirect_uris = ["http://localhost:4200/"]
}

resource "azurerm_key_vault_secret" "graph" {
  key_vault_id = var.key_vault_id
  name         = "graphApi"
  value        = module.azure_ad_b2c.graph_secret
}

resource "random_password" "api_connector_password" {
  length  = 16
  special = true
}

resource "azurerm_key_vault_secret" "api_connector" {
  key_vault_id = var.key_vault_id
  name         = "apiConnectorPassword"
  value        = random_password.api_connector_password.result
}
