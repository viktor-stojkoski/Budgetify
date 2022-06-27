resource "azurerm_aadb2c_directory" "tenant" {
  country_code            = "ML" # Macedonia
  data_residency_location = "Europe"
  display_name            = var.tenant_display_name
  domain_name             = var.tenant_domain_name
  resource_group_name     = var.resource_group_name
  sku_name                = "PremiumP1"
  tags                    = var.tags
}

provider "azuread" {
  tenant_id = azurerm_aadb2c_directory.tenant.tenant_id
}

resource "azuread_application" "app_registration" {
  display_name     = var.app_registration_display_name
  sign_in_audience = "AzureADandPersonalMicrosoftAccount"

  api {
    requested_access_token_version = 2
  }

  single_page_application {
    redirect_uris = var.app_registration_redirect_uris
  }

  web {
    implicit_grant {
      access_token_issuance_enabled = true
      id_token_issuance_enabled     = true
    }
  }
}

resource "azuread_application" "microsoft_graph" {
  display_name = "Microsoft Graph"
}

resource "azuread_application_password" "graph_secret" {
  application_object_id = azuread_application.microsoft_graph.object_id
  display_name          = "Graph Secret"
}
