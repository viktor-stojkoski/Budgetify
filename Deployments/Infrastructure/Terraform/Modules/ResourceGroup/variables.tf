variable "name" {
  type        = string
  description = "Resource group name"
}

variable "location" {
  type        = string
  description = "Resource group location"
  default     = "westeurope"
}

variable "tags" {
  type        = map(string)
  description = "Resource group tags"
}
