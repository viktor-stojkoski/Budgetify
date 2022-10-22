Import-Module $PSScriptRoot\..\..\Utilities\Modules\InvokeGraphApi.psm1 -Force

function New-B2CIdentityUserFlowAttribute {
  <#
    .DESCRIPTION
    Creates new Azure AD B2C user flow attribute.

    .LINK
    https://learn.microsoft.com/en-us/graph/api/identityuserflowattribute-post?view=graph-rest-1.0&tabs=http

    .PARAMETER AccessToken
    Access token for the Microsoft Graph API.

    .PARAMETER DisplayName
    The display name of the user flow attribute.

    .PARAMETER Description
    The description of the user flow attribute. It's shown to the user at the time of sign-up.

    .PARAMETER DataType
    The data type of the user flow attribute. This cannot be modified once the custom user flow attribute is created.
    The supported values for dataType are:
      - string
      - boolean
      - int64

    .OUTPUTS
    Microsoft Graph API Response.

    [PSCustomObject]@{
      id
      displayName
      description
      userFlowAttributeType
      dataType
    }

    .EXAMPLE
    New-B2CIdentityUserFlowAttribute `
      -AccessToken @{ token_type, access_token } `
      -DisplayName "TestAttribute" `
      -Description "Some test attribute description" `
      -DataType "string"
  #>
  param (
    [Parameter(Mandatory = $true)]
    [psobject] $AccessToken,

    [Parameter(Mandatory = $true)]
    [string] $DisplayName,

    [Parameter(Mandatory = $true)]
    [string] $Description,

    [Parameter(Mandatory = $true)]
    [ValidateSet("string", "boolean", "int64")]
    [string] $DataType
  )

  $headers = @{
    "Authorization" = "$($AccessToken.token_type) $($AccessToken.access_token)"
    "Content-Type"  = "application/json"
  }

  $body = @{
    displayName = $DisplayName
    description = $Description
    dataType    = $DataType
  }

  $response = Invoke-MicrosoftGraphApi `
    -Method "POST" `
    -Resource "identity/userFlowAttributes" `
    -Headers $headers `
    -Body $body

  return $response
}

Export-ModuleMember -Function New-B2CIdentityUserFlowAttribute
