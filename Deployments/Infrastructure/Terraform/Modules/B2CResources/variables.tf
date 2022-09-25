variable "tenant_id" {
  type        = string
  description = "B2C Tenant ID"
}

variable "tenant_domain_name" {
  type        = string
  description = "B2C Tenant domain name"
}

variable "angular_app_registration_display_name" {
  type        = string
  description = "Angular app registration display name"
}

variable "angular_app_registration_redirect_uris" {
  type        = set(string)
  description = "Angular app registration redirect URIs"
}

variable "api_app_registration_display_name" {
  type        = string
  description = "API app registration display name"
}
