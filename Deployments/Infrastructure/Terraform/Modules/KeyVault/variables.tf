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
  description = "Key vault location"
}

variable "tags" {
  type        = map(string)
  description = "Key vault tags"
  default     = {}
}

variable "secrets" {
  type        = map(string)
  description = "A map of secrets for the Key Vault"
  default     = {}
}
