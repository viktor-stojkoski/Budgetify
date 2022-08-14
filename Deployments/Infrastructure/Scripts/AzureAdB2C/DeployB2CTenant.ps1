<#
  .DESCRIPTION
  Deploys Azure AD B2C (has to be run with owner privilages).

  .PARAMETER Environment
  Environment to deploy B2C Tenant to.

  .PARAMETER DeployB2CUserFlows
  Boolean indicating whether to deploy B2C User flows or not.
  (Has to be done once!)

  .EXAMPLE
  .\DeployB2CTenant.ps1 `
    -Environment Development
#>

Param(
  [Parameter(Mandatory = $false)]
  [ValidateSet("Development")]
  [string] $Environment,

  [Parameter()]
  [switch] $DeployB2CUserFlows
)

Import-Module $PSScriptRoot\..\Utilities\Modules\InvokeCommand.psm1 -Force

# Set the location to the appropriate working directory for Terraform
$workingDirectory = "$PSScriptRoot\..\..\Terraform\AzureADB2C\$Environment"
Set-Location $workingDirectory

# Terraform init
Write-Host "Running terraform init..." -ForegroundColor red -BackgroundColor white
Invoke-Command {
  terraform init --backend-config=backend.config
}

# Terraform validate
Write-Host "Running terraform validate..." -ForegroundColor red -BackgroundColor white
Invoke-Command {
  terraform validate
}

# Get needed variables
$script:rgName = ""
$script:keyVaultId = ""
Invoke-Command {
  $script:rgName = az group list --query "[?tags.Environment == '$Environment'].name | [0]" 
}
Invoke-Command {
  $script:keyVaultId = az resource list `
    --query "[?type == 'Microsoft.KeyVault/vaults' && tags.Environment == '$Environment'].id | [0]" 
}

Write-Host "Resource group name: $rgName" -ForegroundColor red -BackgroundColor white
Write-Host "Key vault ID: $keyVaultId" -ForegroundColor red -BackgroundColor white

# Terraform plan
Write-Host "Running terraform plan..." -ForegroundColor red -BackgroundColor white
Invoke-Command {
  terraform plan `
    -out tfplan `
    -var "resource_group_name=$rgName" `
    -var "key_vault_id=$keyVaultId"
}

# Terraform apply
Write-Host "Running terraform apply..." -ForegroundColor red -BackgroundColor white
Invoke-Command {
  terraform apply tfplan
}

Remove-Item tfplan

if ($DeployB2CUserFlows.IsPresent) {
  # Get the output
  $output = $(terraform output -json | ConvertFrom-Json)

  # Export output as variables
  foreach ($property in $output.psobject.properties) {
    New-Variable -Name $property.Name -Value $property.Value.value
  }

  Set-Location $PSScriptRoot

  # Deploy B2C User Flows
  $apiConnector = @{
    DisplayName = "Budgetify API"
    TargetUrl   = "https://budgetify.loca.lt/"
    Username    = "BudgetifyUser"
    Password    = "$api_connector_password"
  }

  .\DeployB2CUserFlows.ps1 `
    -ClientId "$graph_client_id" `
    -ClientSecret "$graph_client_secret" `
    -TenantId "$b2c_tenant_domain_name" `
    -ApiConnector $apiConnector
}
