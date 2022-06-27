variable "key_vault_name" {
  type        = string
  description = "Key vault name"
}

variable "resource_group_name" {
  type        = string
  description = "Resource group name"
}

variable "location" {
  type        = string
  description = "Location"
}

variable "tenant_id" {
  type        = string
  description = "Tenant ID that should be used for authenticationg requests to the key vault"
}

variable "object_id" {
  type        = string
  description = "User's object ID for access policy"
}

variable "tags" {
  type        = map(string)
  description = "Tags"
}

variable "secrets" {
  type        = map(string)
  description = "A map of secrets for the Key Vault"
  default     = {}
}
