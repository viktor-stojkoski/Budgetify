Import-Module $PSScriptRoot\NewB2CIdentitiyUserFlow.psm1 -Force
Import-Module $PSScriptRoot\NewB2CUserFlowAttributeAssignment.psm1 -Force

function New-ProfileEditUserFlow {
  <#
    .DESCRIPTION
    Creates new Azure AD B2C Profile Edit user flow.

    .PARAMETER AccessToken
    Access token for the Microsoft Graph API.

    .EXAMPLE
    New-ProfileEditUserFlow `
      -AccessToken @{ token_type, access_token }
  #>
  param (
    [Parameter(Mandatory = $true)]
    [psobject] $AccessToken
  )

  ## Create Profile Edit user flow
  $profileEditFlow = New-B2CIdentitiyUserFlow `
    -AccessToken $AccessToken `
    -Name "ProfileEdit" `
    -Type "profileUpdate"

  ## Create Profile Edit flow user attribute assignments
  # City
  New-B2CUserFlowAttributeAssignment `
    -AccessToken $AccessToken `
    -UserFlowId $profileEditFlow.id `
    -UserInputType "textBox" `
    -UserAttribute @{ id = "city" }

  # Given name
  New-B2CUserFlowAttributeAssignment `
    -AccessToken $AccessToken `
    -UserFlowId $profileEditFlow.id `
    -UserInputType "textBox" `
    -UserAttribute @{ id = "givenName" }

  # Surname
  New-B2CUserFlowAttributeAssignment `
    -AccessToken $AccessToken `
    -UserFlowId $profileEditFlow.id `
    -UserInputType "textBox" `
    -UserAttribute @{ id = "surname" }

  ## Application Claims [NOT YET SUPPORTED]
}

Export-ModuleMember -Function New-ProfileEditUserFlow
