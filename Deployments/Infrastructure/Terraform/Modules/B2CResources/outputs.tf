output "graph_client_id" {
  description = "Microsoft Graph Application Client ID"
  value       = azuread_application.microsoft_graph.application_id
}

output "graph_secret" {
  description = "Microsoft Graph API secret"
  value       = azuread_application_password.graph_secret.value
  sensitive   = true
}
