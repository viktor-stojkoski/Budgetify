variable "tenant_display_name" {
  type        = string
  description = "Tenant display name"
}

variable "resource_group_name" {
  type        = string
  description = "Resource group name"
}

variable "tags" {
  type        = map(string)
  description = "Tenant tags"
}
