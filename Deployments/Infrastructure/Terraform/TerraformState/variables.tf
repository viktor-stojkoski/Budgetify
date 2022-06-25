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

variable "sas_expiry_hours" {
  type        = string
  description = "Expiration date of the token in hours"
  default     = "17520h"
}
