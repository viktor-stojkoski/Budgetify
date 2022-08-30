locals {
  resource_group_name  = "rg-${var.application_name}-tfstate"
  storage_account_name = "sa${var.application_name}tfstate"
}
