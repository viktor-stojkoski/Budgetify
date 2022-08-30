resource "azurerm_storage_account" "sa" {
  name                = var.storage_account_name
  resource_group_name = var.resource_group_name
  location            = var.location
  tags                = var.tags

  account_kind                     = var.account_kind
  account_tier                     = var.account_tier
  account_replication_type         = "LRS"
  min_tls_version                  = "TLS1_2"
  cross_tenant_replication_enabled = false
  is_hns_enabled                   = false
  nfsv3_enabled                    = false

  blob_properties {
    delete_retention_policy {
      days = 7
    }
    container_delete_retention_policy {
      days = 7
    }
    versioning_enabled = false
  }

  network_rules {
    default_action = "Allow"
  }

  large_file_share_enabled          = false
  infrastructure_encryption_enabled = false
}

resource "azurerm_storage_container" "container" {
  name                  = var.container_name
  storage_account_name  = azurerm_storage_account.sa.name
  container_access_type = "private"
}
