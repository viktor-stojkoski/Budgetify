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
    -ApiConnector @{}
#>

Param(
  [Parameter(Mandatory = $true)]
  [string] $ClientId,

  [Parameter(Mandatory = $true)]
  [string] $ClientSecret,

  [Parameter(Mandatory = $true)]
  [string] $TenantId,

  [Parameter(Mandatory = $true)]
  [hashtable] $ApiConnector
)

Import-Module $PSScriptRoot\..\Utilities\Modules\GetGraphApiAccessToken.psm1 -Force
Import-Module $PSScriptRoot\Modules\NewApiConnector.psm1 -Force
Import-Module $PSScriptRoot\Modules\NewSignInSignUpUserFlow.psm1 -Force
Import-Module $PSScriptRoot\Modules\NewProfileEditUserFlow.psm1 -Force
Import-Module $PSScriptRoot\Modules\NewPasswordResetUserFlow.psm1 -Force

try {
  # Write-Host $ClientId
  # Write-Host $ClientSecret
  # Write-Host $TenantId
  # Write-Host $ApiConnector.DisplayName
  # Write-Host $ApiConnector.TargetUrl
  # Write-Host $ApiConnector.Username
  # Write-Host $ApiConnector.Password

  $accessToken = Get-MicrosoftGraphApiAccessToken `
    -ClientId $ClientId `
    -ClientSecret $ClientSecret `
    -TenantId $TenantId

  $apiConnectorResponse = New-ApiConnector `
    -AccessToken $accessToken `
    -DisplayName $ApiConnector.DisplayName `
    -TargetUrl $ApiConnector.TargetUrl `
    -Username $ApiConnector.Username `
    -Password $ApiConnector.Password

  New-SignInSignUpUserFlow `
    -AccessToken $AccessToken `
    -ApiConnectorId $apiConnectorResponse.id

  New-ProfileEditUserFlow `
    -AccessToken $AccessToken

  New-PasswordResetUserFlow `
    -AccessToken $AccessToken

}
catch {
  $exception = $_.Exception
  Write-Host "Creating user flows failed with exception:"
  Write-Host ("Message: " + $exception.Message)
  Write-Host ("Status code: " + $exception.Response.StatusCode)
  Write-Host ("Status description: " + $exception.Response.StatusDescription)
}