<#
  .DESCRIPTION
  Deploy Azure AD B2C user flows.

  .PARAMETER ClientId
  Service principal client ID.

  .PARAMETER ClientSecret
  Service principal client secret.

  .PARAMETER TenantId
  Tenant ID or name.

  .PARAMETER ApiConnector
  [PSCustomObject]@{
    DisplayName
    TargetUrl
    Username
    Password
  }

  .EXAMPLE
  .\DeployB2CUserFlows.ps1 `
    -ClientId "<GUID_HERE>"" `
    -ClientSecret "<SECRET_HERE>"" `
    -TenantId "budgetify.onmicrosoft.com" `
    -CreateUserApiConnector @{} `
    -UpdateUserClaimsApiConnector @{}
#>

Param(
  [Parameter(Mandatory = $true)]
  [string] $ClientId,

  [Parameter(Mandatory = $true)]
  [string] $ClientSecret,

  [Parameter(Mandatory = $true)]
  [string] $TenantId,

  [Parameter(Mandatory = $true)]
  [hashtable] $CreateUserApiConnector,

  [Parameter(Mandatory = $true)]
  [hashtable] $UpdateUserClaimsApiConnector
)

Import-Module $PSScriptRoot\..\Utilities\Modules\GetGraphApiAccessToken.psm1 -Force
Import-Module $PSScriptRoot\Modules\NewApiConnector.psm1 -Force
Import-Module $PSScriptRoot\Modules\NewSignInSignUpUserFlow.psm1 -Force
Import-Module $PSScriptRoot\Modules\NewProfileEditUserFlow.psm1 -Force
Import-Module $PSScriptRoot\Modules\NewPasswordResetUserFlow.psm1 -Force
Import-Module $PSScriptRoot\Modules\NewB2CIdentityUserFlowAttribute.psm1 -Force

$accessToken = Get-MicrosoftGraphApiAccessToken `
  -ClientId $ClientId `
  -ClientSecret $ClientSecret `
  -TenantId $TenantId

$createUserApiConnectorResponse = New-ApiConnector `
  -AccessToken $accessToken `
  -DisplayName $CreateUserApiConnector.DisplayName `
  -TargetUrl $CreateUserApiConnector.TargetUrl `
  -Username $CreateUserApiConnector.Username `
  -Password $CreateUserApiConnector.Password

# Requires manually assigning the API Connector to the SignUpIn User flow
# Before including application claims in token (preview)
New-ApiConnector `
  -AccessToken $accessToken `
  -DisplayName $UpdateUserClaimsApiConnector.DisplayName `
  -TargetUrl $UpdateUserClaimsApiConnector.TargetUrl `
  -Username $UpdateUserClaimsApiConnector.Username `
  -Password $UpdateUserClaimsApiConnector.Password

# Create ID User flow attribute
New-B2CIdentityUserFlowAttribute `
  -AccessToken $accessToken `
  -DisplayName "Id" `
  -Description "User id from Budgetify Database" `
  -DataType "int64"

New-SignInSignUpUserFlow `
  -AccessToken $accessToken `
  -ApiConnectorId $createUserApiConnectorResponse.id

New-ProfileEditUserFlow `
  -AccessToken $accessToken

New-PasswordResetUserFlow `
  -AccessToken $accessToken
