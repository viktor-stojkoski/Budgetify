variable "cognitive_service_name" {
  type        = string
  description = "Cognitive service account name"
}

variable "location" {
  type        = string
  description = "Cognitive service account location"
}

variable "resource_group_name" {
  type        = string
  description = "Resource group name"
}

variable "cognitive_service_kind" {
  type = string
  description = "Cognitive service account kind"
}

variable "tags" {
  type        = map(string)
  description = "Cognitive service account tags"
  default     = {}
}
