locals {
  resource_group_name  = "rg-${var.application_name}-tfstate"
  storage_account_name = "st${var.application_name}tfstate"
}
