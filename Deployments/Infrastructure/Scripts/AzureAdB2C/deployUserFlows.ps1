<#
  .DESCRIPTION
  Deploy API Connector.

  .PARAMETER clientId
  Service principal client ID.

  .PARAMETER clientSecret
  Service principal client secret.

  .PARAMETER tenantId
  Tenant ID.

  .PARAMETER displayName
  API Connector display name.

  .PARAMETER targetUrl
  API Connector target url.

  .PARAMETER userName
  API Connector basic authentication username.

  .PARAMETER password
  API Connector basic authentication password.
#>

Param(
  [Parameter(Mandatory = $true)]
  [string] $clientId,

  [Parameter(Mandatory = $true)]
  [string] $clientSecret,

  [Parameter(Mandatory = $true)]
  [string] $tenantId
)

Import-Module $PSScriptRoot\..\Utilities\Modules\GetGraphApiAccessToken.psm1 -Force
Import-Module $PSScriptRoot\Modules\CreateB2cIdentitiyUserFlow.psm1 -Force

# Get access token
$accessToken = GetMicrosoftGraphApiAccessToken `
  -clientId $clientId `
  -clientSecret $clientSecret `
  -tenantId $tenantId

# Create SignUpIn User flow
CreateB2CIdentityUserFlow `
  -accessToken $accessToken `
  -name "SignUpIn" `
  -type "signUpOrSignIn" `
  -typeVersion 3

# Create ProfileEdit User flow
CreateB2CIdentityUserFlow `
  -accessToken $accessToken `
  -name "ProfileEdit" `
  -type "profileUpdate" `
  -typeVersion 3

# Create PasswordReset User flow
CreateB2CIdentityUserFlow `
  -accessToken $accessToken `
  -name "PasswordReset" `
  -type "passwordReset" `
  -typeVersion 3

