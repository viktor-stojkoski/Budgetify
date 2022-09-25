locals {
  graph_api_permissions = [
    "APIConnectors.ReadWrite.All",
    "IdentityUserFlow.ReadWrite.All"
  ]

  angular_app_permissions = [
    "openid",
    "offline_access"
  ]

  api_app_permissions = [
    "read"
  ]
}
