function InvokeMsGraphApi {
  param (
    [Parameter(Mandatory = $true)]
    [PSObject] $headers,

    [Parameter(Mandatory = $true)]
    [PSObject] $body,

    [Parameter(Mandatory = $true)]
    [string] $uri,

    [Parameter(Mandatory = $true)]
    [ValidateSet("GET", "POST")]
    [string] $method,

    [Parameter(Mandatory = $false)]
    [string] $contentType = "application/json"
  )

  $bodyJson = ConvertTo-Json $body
  
  try {
    $response = Invoke-RestMethod `
      -Uri $uri `
      -Method $method `
      -Headers $headers `
      -Body $bodyJson `
      -ContentType $contentType
  
    $response
  }
  catch {
    $exception = $_.Exception
    Write-Host "Executing Microsoft Graph API throw an exception:"
    Write-Host ("Message: " + $exception.Message)
    Write-Host ("Status code: " + $exception.Response.StatusCode)
    Write-Host ("Status description: " + $exception.Response.StatusDescription)
  }
}
