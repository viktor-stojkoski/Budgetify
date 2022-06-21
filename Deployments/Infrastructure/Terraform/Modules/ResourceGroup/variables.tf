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
  type        = map()
  description = "Resource group tags"
  default = {
    "Environment" = "Development"
    "StartDate"   = formatdate("DD.MM.YYYY", timestamp())
  }
}
