locals {
  tags = {
    DeployedOn  = formatdate("DD.MM.YYYY", timestamp())
    Environment = terraform.workspace
  }

  resource_group_name  = lower("rg-${var.application_name}-dev")
  storage_account_name = lower("st${var.application_name}dev")
  key_vault_name       = lower("kv-${var.application_name}dev") #TODO: add dash before dev
}
