locals {
  graph_api_permissions = [
    "APIConnectors.ReadWrite.All",
    "IdentityUserFlow.ReadWrite.All"
  ]

  angular_app_permissions = [
    "37f7f235-527c-4136-accd-4a02d197296e", // openid
    "7427e0e9-2fba-42fe-b0c0-848c9e6a8182"  // offline_access
  ]
}
