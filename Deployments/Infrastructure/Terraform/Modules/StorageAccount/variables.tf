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

variable "account_tier" {
  type        = string
  description = "Storage account tier (Standard, Premium)"
  default     = "Premium"
}

variable "account_kind" {
  type        = string
  description = "Storage account kind (StorageV2, BlockBlobStorage..)"
  default     = "BlockBlobStorage"
}

variable "tags" {
  type        = map(string)
  description = "Storage account tags"
}

variable "container_name" {
  type        = string
  description = "Container name"
}
