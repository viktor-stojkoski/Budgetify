Import-Module $PSScriptRoot\..\..\Utilities\Modules\InvokeGraphApi.psm1 -Force

function New-ApiConnector {
  <#
  .DESCRIPTION
  Creates new API Connector.

  .LINK
  https://docs.microsoft.com/en-us/graph/api/identityapiconnector-create?view=graph-rest-1.0&tabs=http

  .PARAMETER AccessToken
  Access token for the Microsoft Graph API.

  .PARAMETER DisplayName
  API Connector display name.

  .PARAMETER TargetUrl
  API Connector target url.

  .PARAMETER UserName
  API Connector basic authentication username.

  .PARAMETER Password
  API Connector basic authentication password.

  .OUTPUTS
    Microsoft Graph API Response.

    [PSCustomObject]@{
      @odata.context
      id
      displayName
      targetUrl
      authenticationConfiguration = @{
        @odata.type
        username
        password
      }
    }
#>

  Param(
    [Parameter(Mandatory = $true)]
    [psobject] $AccessToken,

    [Parameter(Mandatory = $true)]
    [string] $DisplayName,

    [Parameter(Mandatory = $true)]
    [string] $TargetUrl,

    [Parameter(Mandatory = $true)]
    [string] $Username,

    [Parameter(Mandatory = $true)]
    [securestring] $Password
  )

  $headers = @{
    'Authorization' = "$($AccessToken.token_type) $($AccessToken.access_token)"
    'Content-Type'  = 'application/json'
  }

  $body = @{
    displayName                 = $DisplayName
    targetUrl                   = $TargetUrl
    authenticationConfiguration = @{
      "@odata.type" = "#microsoft.graph.basicAuthentication"
      username      = $Username
      password      = (ConvertFrom-SecureString $Password)
    }
  }

  $response = Invoke-MicrosoftGraphApi `
    -Method "POST" `
    -Resource "identity/apiConnectors" `
    -Headers $headers `
    -Body $body

  return $response
}

Export-ModuleMember -Function New-ApiConnector
