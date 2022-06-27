output "graph_secret" {
  description = "Microsoft Graph API secret"
  value       = azuread_application_password.graph_secret.value
  sensitive   = true
}
