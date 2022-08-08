function Invoke-MicrosoftGraphApi {
  <#
    .DESCRIPTION
    Invokes the Microsoft Graph API with the given parameters.

    .PARAMETER Method
    Microsoft Graph API HTTP request method.

    .PARAMETER Version
    The version of the Microsoft Graph API your application is using.

    .PARAMETER Resource
    A resource can be an entity or complex type, commonly defined with properties.

    .PARAMETER Headers
    Microsoft Graph API HTTP request headers.

    .PARAMETER Body
    Microsoft Graph API HTTP request body.

    .PARAMETER ContentType
    Microsoft Graph API content type.

    .OUTPUTS
    Microsoft Graph API Response.

    [PSCustomObject]

    .EXAMPLE
    Invoke-MicrosoftGraphApi `
      -Method "POST" `
      -Version "v1.0" `
      -Resource "identity/apiConnectors"
      -Headers @{
        "Authorization" = "Bearer <TOKEN_HERE>"
        "Content-Type"  = "application/json"
      }
      -Body @{
        displayName = "Test"
      }
  #>
  param (
    [Parameter(Mandatory = $true)]
    [ValidateSet("GET", "POST", "PATCH", "PUT", "DELETE")]
    [string] $Method,

    [ValidateSet("v1.0", "beta")]
    [Parameter(Mandatory = $false)]
    [string] $Version = "v1.0",

    [Parameter(Mandatory = $true)]
    [string] $Resource,

    [Parameter(Mandatory = $true)]
    [psobject] $Headers,

    [Parameter(Mandatory = $false)]
    [psobject] $Body,

    [Parameter(Mandatory = $false)]
    [string] $ContentType = "application/json"
  )

  $params = @{
    Uri         = "https://graph.microsoft.com/$Version/$Resource"
    Method      = $Method
    Headers     = $Headers
    ContentType = $ContentType
  }

  if ($Body) {
    $bodyJson = ConvertTo-Json $Body

    $params.Body = $bodyJson
  }

  try {
    Write-Host @params
    $response = Invoke-RestMethod @params

    return $response
  }
  catch {
    $exception = $_.Exception
    Write-Host "Executing Microsoft Graph API failed with exception:"
    Write-Host ("Message: " + $exception.Message)
    Write-Host ("Status code: " + $exception.Response.StatusCode)
    Write-Host ("Status description: " + $exception.Response.StatusDescription)
    throw "Executing Microsoft Graph API failed with exception."
  }
}

Export-ModuleMember -Function Invoke-MicrosoftGraphApi
