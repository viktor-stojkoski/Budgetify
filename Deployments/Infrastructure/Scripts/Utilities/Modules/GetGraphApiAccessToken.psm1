function GetMicrosoftGraphApiAccessToken {
  <#
    .DESCRIPTION
    Returns Microsoft Graph Api AccessToke
  #>
  param (
    [Parameter(Mandatory = $true)]
    [string] $clientId,
  
    [Parameter(Mandatory = $true)]
    [string] $clientSecret,
  
    [Parameter(Mandatory = $true)]
    [string] $tenantId
  )
  
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

  return $authResponse;
}