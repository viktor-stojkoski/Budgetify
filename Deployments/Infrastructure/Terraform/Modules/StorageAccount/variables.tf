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
  default     = "Standard"
}

variable "account_kind" {
  type        = string
  description = "Storage account kind (StorageV2, BlockBlobStorage..)"
  default     = "StorageV2"
}

variable "tags" {
  type        = map(string)
  description = "Storage account tags"
  default     = {}
}

variable "application_container_name" {
  type        = string
  description = "Application container name"
}

variable "form_recognizer_container_name" {
  type        = string
  description = "Form Recognizer (CognitiveService) container name"
}
