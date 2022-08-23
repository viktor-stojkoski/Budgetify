data "azuread_application_published_app_ids" "well_known" {}

resource "azuread_service_principal" "microsoft_graph" {
  application_id = data.azuread_application_published_app_ids.well_known.result.MicrosoftGraph
  use_existing   = true
}

resource "azuread_application" "app_registration" {
  display_name                   = var.app_registration_display_name
  sign_in_audience               = "AzureADandPersonalMicrosoftAccount"
  fallback_public_client_enabled = true

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

  required_resource_access {
    resource_app_id = data.azuread_application_published_app_ids.well_known.result.MicrosoftGraph
    resource_access {
      id   = azuread_service_principal.microsoft_graph.oauth2_permission_scope_ids[local.angular_app_permissions[0]]
      type = "Scope"
    }

    resource_access {
      id   = azuread_service_principal.microsoft_graph.oauth2_permission_scope_ids[local.angular_app_permissions[1]]
      type = "Scope"
    }
  }
}

resource "azuread_service_principal" "app_registration" {
  application_id = azuread_application.app_registration.application_id
  use_existing   = true
}

resource "azuread_service_principal_delegated_permission_grant" "app_registration" {
  service_principal_object_id          = azuread_service_principal.app_registration.object_id
  resource_service_principal_object_id = azuread_service_principal.microsoft_graph.object_id
  claim_values                         = local.angular_app_permissions
}

resource "azuread_application" "microsoft_graph" {
  display_name = "Microsoft Graph API"

  required_resource_access {
    resource_app_id = data.azuread_application_published_app_ids.well_known.result.MicrosoftGraph

    resource_access {
      id   = azuread_service_principal.microsoft_graph.app_role_ids[local.graph_api_permissions[0]]
      type = "Role"
    }

    resource_access {
      id   = azuread_service_principal.microsoft_graph.app_role_ids[local.graph_api_permissions[1]]
      type = "Role"
    }
  }
}

resource "azuread_service_principal" "microsoft_graph_sp" {
  application_id = azuread_application.microsoft_graph.application_id
}

resource "azuread_app_role_assignment" "graph_role_assignment" {
  for_each            = toset(local.graph_api_permissions)
  app_role_id         = azuread_service_principal.microsoft_graph.app_role_ids[each.key]
  principal_object_id = azuread_service_principal.microsoft_graph_sp.object_id
  resource_object_id  = azuread_service_principal.microsoft_graph.object_id
}

resource "azuread_application_password" "graph_secret" {
  application_object_id = azuread_application.microsoft_graph.object_id
  display_name          = "Graph Secret"
}
