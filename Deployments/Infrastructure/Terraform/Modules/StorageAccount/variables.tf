variable "resource_group_name" {
  type        = string
  description = "Resource group name"
}

variable "storage_account_name" {
  type        = string
  description = "Storage account name"
}

variable "location" {
  type        = string
  description = "Storage account location"
}

variable "tags" {
  type        = map()
  description = "Storage account tags"
  default = {
    "Environment" = "Development"
    "StartDate"   = formatdate("DD.MM.YYYY", timestamp())
  }
}

variable "container_name" {
  type        = string
  description = "Container name"
  default     = "budgetify"
}
