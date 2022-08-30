Import-Module $PSScriptRoot\NewB2CIdentitiyUserFlow.psm1 -Force
Import-Module $PSScriptRoot\NewB2CUserFlowAttributeAssignment.psm1 -Force
Import-Module $PSScriptRoot\SetB2CUserFlowApiConnector.psm1 -Force

function New-SignInSignUpUserFlow {
  <#
    .DESCRIPTION
    Creates new Azure AD B2C SignUp and SignIn user flow.

    .PARAMETER AccessToken
    Access token for the Microsoft Graph API.

    .PARAMETER ApiConnectorId
    ID of the API Connector to update the B2C User Flow with.

    .EXAMPLE
    New-SignInSignUpUserFlow `
      -AccessToken @{ token_type, access_token } `
      -ApiConnectorId <ID_HERE>
  #>
  param (
    [Parameter(Mandatory = $true)]
    [psobject] $AccessToken,

    [Parameter(Mandatory = $true)]
    [string] $ApiConnectorId
  )

  ## Create SignUpIn user flow
  $signUpSignInFlow = New-B2CIdentityUserFlow `
    -AccessToken $AccessToken `
    -Name "SignUpIn" `
    -Type "signUpOrSignIn"

  ## Create SignUpSignIn flow user attribute assignments
  # City
  New-B2CUserFlowAttributeAssignment `
    -DisplayName "City" `
    -AccessToken $AccessToken `
    -UserFlowId $signUpSignInFlow.id `
    -UserInputType "textBox" `
    -UserAttribute @{ id = "city" }

  # Given name
  New-B2CUserFlowAttributeAssignment `
    -DisplayName "First name" `
    -AccessToken $AccessToken `
    -UserFlowId $signUpSignInFlow.id `
    -UserInputType "textBox" `
    -UserAttribute @{ id = "givenName" }

  # Surname
  New-B2CUserFlowAttributeAssignment `
    -DisplayName "Last name" `
    -AccessToken $AccessToken `
    -UserFlowId $signUpSignInFlow.id `
    -UserInputType "textBox" `
    -UserAttribute @{ id = "surname" }

  ## Application Claims [NOT YET SUPPORTED]

  ## API Connector
  Set-B2CUserFlowApiConnector `
    -AccessToken $AccessToken `
    -UserFlowId $signUpSignInFlow.id `
    -ApiConnectorId $ApiConnectorId
}

Export-ModuleMember -Function New-SignInSignUpUserFlow
