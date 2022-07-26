variable "location" {
  type        = string
  description = "Location"
  default     = "West Europe"
}

variable "tags" {
  type        = map(string)
  description = "Resource tags"
}

variable "application_name" {
  type        = string
  description = "Name of the application"
  default     = "budgetify"
}

variable "azure_subscription" {
  type        = string
  description = "Subscription name or ID"
}

variable "tenant_id" {
  type        = string
  description = "Tenant id"
}

variable "object_id" {
  type        = string
  description = "Principal object ID (User)"
}

variable "client_id" {
  type        = string
  description = "Client ID"
  sensitive   = true
}

variable "client_secret" {
  type        = string
  description = "Client Secret"
  sensitive   = true
}
