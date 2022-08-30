locals {
  all_key_permissions = [
    "Get", "List", "Update", "Create", "Import", "Delete", "Recover", "Backup", "Restore",
    "Decrypt", "Encrypt", "UnwrapKey", "WrapKey", "Verify", "Sign",
    "Purge"
  ]
  all_secret_permissions = [
    "Get", "List", "Set", "Delete", "Recover", "Backup", "Restore",
    "Purge"
  ]
  all_certificate_permissions = [
    "Get",
    "List",
    "Update",
    "Create",
    "Import",
    "Delete",
    "Recover",
    "Backup",
    "Restore",
    "ManageContacts",
    "ManageIssuers",
    "GetIssuers",
    "ListIssuers",
    "SetIssuers",
    "DeleteIssuers",

    "Purge"
  ]

  viktor_stojkoski_object_id = "c09a798b-a958-4d5c-8ae9-7f8388b274d2"
}
