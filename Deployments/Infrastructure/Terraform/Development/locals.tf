locals {
  tags = {
    DeployedOn  = formatdate("DD.MM.YYYY", timestamp())
    Environment = "Development"
  }

  resource_group_name            = lower("rg-${var.application_name}-dev")
  storage_account_name           = lower("sa${var.application_name}dev")
  key_vault_name                 = lower("kv-${var.application_name}-dev")
  form_recognizer_name           = lower("fr-${var.application_name}-dev")
  form_recognizer_container_name = "form-recognizer"
}
