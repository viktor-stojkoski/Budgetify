variable "tenant_display_name" {
  type        = string
  description = "Tenant display name"
}

variable "tenant_domain_name" {
  type        = string
  description = "Tenant domain name including the .onmicrosoft suffix"
}

variable "resource_group_name" {
  type        = string
  description = "Resource group name"
}

variable "tags" {
  type        = map(string)
  description = "Resource group tags"
}

variable "app_registration_display_name" {
  type        = string
  description = "App registration display name"
}

variable "app_registration_redirect_uris" {
  type        = set(string)
  description = "App registration redirect URIs"
}
