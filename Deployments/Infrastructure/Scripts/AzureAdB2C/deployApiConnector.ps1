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
  [string] $tenantId,

  [Parameter(Mandatory = $true)]
  [string] $displayName,

  [Parameter(Mandatory = $true)]
  [string] $targetUrl,

  [Parameter(Mandatory = $true)]
  [string] $userName,

  [Parameter(Mandatory = $true)]
  [string] $password #TODO: Check SecureString
)

# Get access token for Microsoft Graph API
$tokenRequestBody = @{ 
  grant_type    = "client_credentials"
  scope         = "https://graph.microsoft.com/.default"
  client_id     = $clientId
  client_secret = $clientSecret
}

$authResponse = Invoke-RestMethod `
  -Uri "https://login.microsoftonline.com/$($tenantId)/oauth2/v2.0/token" `
  -Method Post `
  -Body $tokenRequestBody

$headers = @{
  'Authorization' = "$($authResponse.token_type) $($authResponse.access_token)"
  'Content-Type'  = 'application/json'
}

$bodyObj = @{
  displayName                 = $displayName
  targetUrl                   = $targetUrl
  authenticationConfiguration = @{
    "@odata.type" = "#microsoft.graph.basicAuthentication"
    username      = $userName
    password      = $password
  }
}
$body = ConvertTo-Json $bodyObj

try {
  # Create API Connector
  $response = Invoke-RestMethod `
    -Uri "https://graph.microsoft.com/v1.0/identity/apiConnectors" `
    -Method Post `
    -Headers $headers `
    -Body $body `
    -ContentType 'application/json'

  $response
}
catch {
  $exception = $_.Exception
  Write-Host "Creating API Connector failed with exception:"
  Write-Host ("Message: " + $exception.Message)
  Write-Host ("Status code: " + $exception.Response.StatusCode)
  Write-Host ("Status description: " + $exception.Response.StatusDescription)
}
  