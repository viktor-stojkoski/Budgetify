Import-Module $PSScriptRoot\..\..\Utilities\Modules\InvokeMsGraphApi.psm1

function CreateB2CIdentityUserFlow {
  param (
    [Parameter(Mandatory = $true)]
    [Object] $accessToken,

    [Parameter(Mandatory = $true)]
    [string] $name,
  
    [Parameter(Mandatory = $true)]
    [ValidateSet("signUp", "signIn", "signUpOrSignIn", "passwordReset", "profileUpdate", "resourceOwner")]
    [string] $type,
  
    [Parameter(Mandatory = $true)]
    [float] $typeVersion,

    [Parameter(Mandatory = $false)]
    [bool] $isLanguageCustomizationEnabled = $false,
    
    [Parameter(Mandatory = $false)]
    [string] $defaultLanguageTag
  )
  
  $headers = @{
    'Authorization' = "$($accessToken.token_type) $($accessToken.access_token)"
    'Content-Type'  = 'application/json'
  }

  $body = @{
    id                             = $name
    userFlowType                   = $type
    userflowTypeVersion            = $typeVersion
    isLanguageCustomizationEnabled = $isLanguageCustomizationEnabled
    defaultLanguageTag             = $defaultLanguageTag
  }

  InvokeMsGraphApi `
    -headers $headers `
    -body $body `
    -uri "https://graph.microsoft.com/beta/identity/b2cUserFlows" `
    -method "POST"
}