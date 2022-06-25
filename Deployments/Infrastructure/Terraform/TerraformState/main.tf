terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
    }
  }
}

provider "azurerm" {
  features {}
}

module "resource_group" {
  source   = "../Modules/ResourceGroup"
  name     = local.resource_group_name
  location = var.location
  tags     = var.tags
}

module "storage_account" {
  source               = "../Modules/StorageAccount"
  resource_group_name  = module.resource_group.resource_group_name
  location             = var.location
  storage_account_name = local.storage_account_name
  account_tier         = "Standard"
  account_kind         = "StorageV2"
  container_name       = "terraform-state"
  tags                 = var.tags
}

# data "azurerm_storage_account_sas" "state" {
#   connection_string = module.storage_account.storage_account_connection_string
#   https_only        = true

#   resource_types {
#     container = true
#     object    = true
#     service   = true
#   }

#   services {
#     blob  = true
#     file  = false
#     queue = false
#     table = false
#   }

#   start  = timestamp()
#   expiry = timeadd(timestamp(), var.sas_expiry_hours)

#   permissions {
#     read    = true
#     write   = true
#     delete  = true
#     list    = true
#     add     = true
#     create  = true
#     update  = false
#     process = false
#     filter  = false
#     tag     = false
#   }
# }

# resource "local_file" "post-config" {
#   depends_on = [module.storage_account.storage_container]

#   filename = "${path.module}/backend-config.txt"
#   content  = <<EOF
#     storage_account_name = "${module.storage_account.storage_account_name}"
#     container_name = "terraform-state"
#     key = "terraform.tfstate"
#     sas_token = "${data.azurerm_storage_account_sas.state.sas}"
#   EOF
# }
