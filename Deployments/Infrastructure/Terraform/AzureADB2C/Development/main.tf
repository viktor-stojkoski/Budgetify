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

module "b2c_tenant" {
  source              = "../../Modules/B2CTenant"
  tenant_display_name = "${var.application_name} Dev"
  resource_group_name = var.resource_group_name
  tags = {
    StartDate   = "30.9.2021"
    DeployedBy  = "Terraform"
    Environment = "Development"
  }
}

module "azure_ad_b2c" {
  source                                 = "../../Modules/B2CResources"
  tenant_id                              = module.b2c_tenant.tenant_id
  tenant_domain_name                     = module.b2c_tenant.tenant_domain_name
  angular_app_registration_display_name  = "${var.application_name} Angular"
  angular_app_registration_redirect_uris = ["http://localhost:4200/"]
  api_app_registration_display_name      = "${var.application_name} API"
}

resource "azurerm_key_vault_secret" "graph" {
  key_vault_id = var.key_vault_id
  name         = "graphApi"
  value        = module.azure_ad_b2c.graph_secret
}

resource "random_password" "api_connector" {
  length  = 16
  special = true
}

resource "azurerm_key_vault_secret" "api_connector" {
  key_vault_id = var.key_vault_id
  name         = "apiConnectorPassword"
  value        = random_password.api_connector.result
}
