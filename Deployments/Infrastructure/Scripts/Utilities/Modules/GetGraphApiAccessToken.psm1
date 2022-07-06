function Get-MicrosoftGraphApiAccessToken {
  <#
    .DESCRIPTION
    Returns Microsoft Graph Api AccessToken.

    .LINK
    https://docs.microsoft.com/en-us/graph/auth-v2-service

    .PARAMETER ClientId
    Application Client ID.

    .PARAMETER ClientSecret
    Application Client Secret.

    .PARAMETER TenantId
    Directory (tenant) ID.

    .OUTPUTS
    Auth Response with auth token.

    [PSCustomObject]@{
      token_type
      expires_in
      ext_expires_in
      access_token
    }

    .EXAMPLE
    Get-MicrosoftGraphApiAccessToken `
      -ClientId "SOMEGUID" `
      -ClientSecret "SECRET" `
      -TenantId "budgetify.onmicrosoft.com"
  #>
  param (
    [Parameter(Mandatory = $true)]
    [guid] $ClientId,

    [Parameter(Mandatory = $true)]
    [string] $ClientSecret,

    [Parameter(Mandatory = $true)]
    [string] $TenantId
  )

  $tokenRequestBody = @{
    grant_type    = "client_credentials"
    scope         = "https://graph.microsoft.com/.default"
    client_id     = $ClientId
    client_secret = $ClientSecret
  }

  $authResponse = Invoke-RestMethod `
    -Uri "https://login.microsoftonline.com/$($TenantId)/oauth2/v2.0/token" `
    -Method Post `
    -Body $tokenRequestBody

  return $authResponse;
}

Export-ModuleMember -Function Get-MicrosoftGraphApiAccessToken
