variable "resource_group_name" {
  type        = string
  description = "Resource group name"
}

variable "location" {
  type        = string
  description = "Resource location"
}

variable "container_registry_name" {
  type        = string
  description = "Container registry name"
}

variable "sku" {
  type        = string
  description = "Container registry SKU"
  default     = "Basic"
}

variable "tags" {
  type        = map(string)
  description = "Resource tags"
}
