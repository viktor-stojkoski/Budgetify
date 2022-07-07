Import-Module $PSScriptRoot\NewB2CIdentitiyUserFlow.psm1 -Force

function New-PasswordResetUserFlow {
  <#
    .DESCRIPTION
    Creates new Azure AD B2C Password Reset user flow.

    .PARAMETER AccessToken
    Access token for the Microsoft Graph API.

    .EXAMPLE
    New-PasswordResetUserFlow `
      -AccessToken @{ token_type, access_token }
  #>
  param (
    [Parameter(Mandatory = $true)]
    [psobject] $AccessToken
  )

  ## Create Password Reset user flow
  New-B2CIdentitiyUserFlow `
    -AccessToken $AccessToken `
    -Name "PasswordReset" `
    -Type "passwordReset"

  ## Application Claims [NOT YET SUPPORTED]
}

Export-ModuleMember -Function New-PasswordResetUserFlow
