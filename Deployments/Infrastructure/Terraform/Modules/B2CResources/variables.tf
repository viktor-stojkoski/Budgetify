# variable "tenant_id" {
#   type        = string
#   description = "B2C Tenant ID"
# }

variable "app_registration_display_name" {
  type        = string
  description = "App registration display name"
}

variable "app_registration_redirect_uris" {
  type        = set(string)
  description = "App registration redirect URIs"
}
