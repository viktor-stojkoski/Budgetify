Import-Module $PSScriptRoot\..\..\Utilities\Modules\InvokeGraphApi.psm1 -Force

function New-B2CUserFlowAttributeAssignment {
  <#
    .DESCRIPTION
    Creates new B2C user flow attribute assignment.

    .LINK
    https://docs.microsoft.com/en-us/graph/api/b2cidentityuserflow-post-userattributeassignments?view=graph-rest-beta&tabs=http

    .PARAMETER AccessToken
    Access token for the Microsoft Graph API.

    .PARAMETER UserFlowId
    The name of the user flow to create B2C user attribute assignment.

    .PARAMETER DisplayName
    The display name of the identityUserFlowAttribute within a user flow.

    .PARAMETER IsOptional
    Determines whether the identityUserFlowAttribute is optional.

    .PARAMETER RequiresVerification
    Determines whether the identityUserFlowAttribute requires verification.
    This is only used for verifying the user's phone number or email address.

    .PARAMETER UserAttributeValues
    The input options for the user flow attribute.
    Only applicable when the userInputType is
      - radioSingleSelect,
      - dropdownSingleSelect,
      - checkboxMultiSelect.

    .PARAMETER UserInputType
    The input type of the user flow attribute.

    .PARAMETER UserAttribute
    The identifier for the user flow attribute to include in the user flow assignment.

    .OUTPUTS
    Microsoft Graph API Response.

    [PSCustomObject]@{
      id
      isOptional
      requiresVerification
      userInputType
      displayName
      userAttributeValues = @()
    }

    .EXAMPLE
    New-B2CUserFlowAttributeAssignment `
      -AccessToken @{ token_type, access_token } `
      -UserFlowId "B2C_1_SignUpIn" `
      -UserInputType "textBox" `
      -UserAttribute @{ id = "city" }
  #>
  param (
    [Parameter(Mandatory = $true)]
    [psobject] $AccessToken,

    [Parameter(Mandatory = $true)]
    [string] $UserFlowId,

    [Parameter(Mandatory = $false)]
    [string] $DisplayName,

    [Parameter(Mandatory = $false)]
    [switch] $IsOptional,

    [Parameter(Mandatory = $false)]
    [switch] $RequiresVerification,

    [Parameter(Mandatory = $false)]
    [array] $UserAttributeValues = @(),

    [Parameter(Mandatory = $true)]
    [ValidateSet("textBox", "dateTimeDropdown", "radioSingleSelect", "dropdownSingleSelect", "emailBox", "checkboxMultiSelect")]
    [string] $UserInputType,

    [Parameter(Mandatory = $true)]
    [psobject] $UserAttribute
  )

  $headers = @{
    "Authorization" = "$($AccessToken.token_type) $($AccessToken.access_token)"
    "Content-Type"  = "application/json"
  }

  $body = @{
    displayName          = $DisplayName
    isOptional           = $IsOptional.IsPresent
    requiresVerification = $RequiresVerification.IsPresent
    userAttributeValues  = $UserAttributeValues
    userInputType        = $UserInputType
    userAttribute        = $UserAttribute
  }

  $response = Invoke-MicrosoftGraphApi `
    -Method "POST" `
    -Version "beta" `
    -Resource "identity/b2cUserFlows/$UserFlowId/userAttributeAssignments" `
    -Headers $headers `
    -Body $body

  return $response
}

Export-ModuleMember -Function New-B2CUserFlowAttributeAssignment
