provider "azuread" {
  tenant_id = var.tenant_id
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
  display_name = "Microsoft Graph API"

  required_resource_access {
    resource_app_id = "00000003-0000-0000-c000-000000000000" # Microsoft Graph

    resource_access {
      id   = "1dfe531a-24a6-4f1b-80f4-7a0dc5a0a171" # APIConnectors.ReadWrite.All
      type = "Role"
    }

    resource_access {
      id   = "65319a09-a2be-469d-8782-f6b07debf789" # IdentityUserFlow.ReadWrite.All
      type = "Role"
    }
  }
}

resource "null_resource" "grant_admin_consent" {
  depends_on = [
    azuread_application.microsoft_graph
  ]
  provisioner "local-exec" {
    command = "az ad app permission admin-consent --id ${azuread_application.microsoft_graph.application_id}"
  }
}

resource "azuread_application_password" "graph_secret" {
  application_object_id = azuread_application.microsoft_graph.object_id
  display_name          = "Graph Secret"
}
