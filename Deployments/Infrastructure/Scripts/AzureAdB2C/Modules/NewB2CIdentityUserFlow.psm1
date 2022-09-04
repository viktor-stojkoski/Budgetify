Import-Module $PSScriptRoot\..\..\Utilities\Modules\InvokeGraphApi.psm1 -Force

function New-B2CIdentityUserFlow {
  <#
    .DESCRIPTION
    Creates new Azure AD B2C user flow.

    .LINK
    https://docs.microsoft.com/en-us/graph/api/identitycontainer-post-b2cuserflows?view=graph-rest-beta&tabs=http

    .PARAMETER AccessToken
    Access token for the Microsoft Graph API.

    .PARAMETER Name
    The name of the user flow. The name will be pre-pended with B2C_1_ after creation
    if the prefix was not added to the name during your request.

    .PARAMETER Type
    The type of user flow you are creating.

    .PARAMETER TypeVersion
    The version of the user flow.

    .PARAMETER IsLanguageCustomizationEnabled
    Determines whether language customization is enabled within the Azure AD B2C user flow.
    Not enabled by default.

    .PARAMETER DefaultLanguageTag
    Specifies the default language of the b2cIdentityUserFlow.

    .OUTPUTS
    Microsoft Graph API Response.

    [PSCustomObject]@{
      @odata.context
      id
      userFlowType
      userFlowTypeVersion
      isLanguageCustomizationEnabled
      defaultLanguageTag
      authenticationMethods
      tokenClaimsConfiguration = @{
        isIssuerEntityUserFlow
      }
      apiConnectorConfiguration = @{}
    }

    .EXAMPLE
    New-B2CIdentityUserFlow `
      -AccessToken @{ token_type, access_token } `
      -Name "SignUpIn" `
      -Type "signUpOrSignIn"
  #>
  param (
    [Parameter(Mandatory = $true)]
    [psobject] $AccessToken,

    [Parameter(Mandatory = $true)]
    [string] $Name,

    [Parameter(Mandatory = $true)]
    [ValidateSet("signUp", "signIn", "signUpOrSignIn", "passwordReset", "profileUpdate", "resourceOwner")]
    [string] $Type,

    [Parameter(Mandatory = $false)]
    [float] $TypeVersion = 3,

    [Parameter(Mandatory = $false)]
    [bool] $IsLanguageCustomizationEnabled = $false,

    [Parameter(Mandatory = $false)]
    [string] $DefaultLanguageTag
  )

  $headers = @{
    "Authorization" = "$($AccessToken.token_type) $($AccessToken.access_token)"
    "Content-Type"  = "application/json"
  }

  $body = @{
    id                             = $Name
    userFlowType                   = $Type
    userFlowTypeVersion            = $TypeVersion
    isLanguageCustomizationEnabled = $IsLanguageCustomizationEnabled
    defaultLanguageTag             = $DefaultLanguageTag
  }

  $response = Invoke-MicrosoftGraphApi `
    -Method "POST" `
    -Version "beta" `
    -Resource "identity/b2cUserFlows" `
    -Headers $headers `
    -Body $body

  return $response
}

Export-ModuleMember -Function New-B2CIdentityUserFlow
