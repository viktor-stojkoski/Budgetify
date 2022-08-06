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
  default     = "Budgetifytest"
}

variable "azure_subscription" {
  type        = string
  description = "Subscription name or ID"
}
