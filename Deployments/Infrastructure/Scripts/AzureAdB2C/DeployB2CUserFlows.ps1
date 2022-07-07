<#
  .DESCRIPTION
  Deploy Azure AD B2C user flows.

  .PARAMETER ClientId
  Service principal client ID.

  .PARAMETER ClientSecret
  Service principal client secret.

  .PARAMETER TenantId
  Tenant ID or name.

  .PARAMETER ApiConnectorId
  ID of the API Connector to update the B2C User Flow with.

  .EXAMPLE
  .\DeployB2CUserFlows.ps1 `
    -ClientId "<GUID_HERE>"" `
    -ClientSecret "<SECRET_HERE>"" `
    -TenantId "budgetify.onmicrosoft.com" `
    -ApiConnectorId "<API_CONNECTOR_ID>"
#>

Param(
  [Parameter(Mandatory = $true)]
  [string] $ClientId,

  [Parameter(Mandatory = $true)]
  [string] $ClientSecret,

  [Parameter(Mandatory = $true)]
  [string] $TenantId,

  [Parameter(Mandatory = $true)]
  [string] $ApiConnectorId
)

Import-Module $PSScriptRoot\..\Utilities\Modules\GetGraphApiAccessToken.psm1 -Force
Import-Module $PSScriptRoot\Modules\NewSignInSignUpUserFlow.psm1 -Force
Import-Module $PSScriptRoot\Modules\NewProfileEditUserFlow.psm1 -Force
Import-Module $PSScriptRoot\Modules\NewPasswordResetUserFlow.psm1 -Force

$accessToken = Get-MicrosoftGraphApiAccessToken `
  -ClientId $ClientId `
  -ClientSecret $ClientSecret `
  -TenantId $TenantId

New-SignInSignUpUserFlow `
  -AccessToken $AccessToken `
  -ApiConnectorId $ApiConnectorId

New-ProfileEditUserFlow `
  -AccessToken $AccessToken

New-PasswordResetUserFlow `
  -AccessToken $AccessToken
