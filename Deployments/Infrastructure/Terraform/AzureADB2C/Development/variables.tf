variable "resource_group_name" {
  type        = string
  description = "Name of the resource group."
}

variable "application_name" {
  type        = string
  description = "Name of the application"
  default     = "Budgetify"
}

variable "key_vault_id" {
  type        = string
  description = "ID of the Key Vault for the secrets to be created in."
}

variable "tenant_id" {
  type        = string
  description = "ID of the tenant the resources should be created in."
}
