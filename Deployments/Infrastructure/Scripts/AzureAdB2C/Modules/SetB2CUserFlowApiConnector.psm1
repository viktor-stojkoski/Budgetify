Import-Module $PSScriptRoot\..\..\Utilities\Modules\InvokeGraphApi.psm1 -Force

function Set-B2CUserFlowApiConnector {
  <#
    .DESCRIPTION
    Updates the API Connector Configuration in the given B2C User Flow.

    .LINK
    https://docs.microsoft.com/en-us/graph/api/b2cidentityuserflow-put-apiconnectorconfiguration?view=graph-rest-beta&tabs=http

    .PARAMETER AccessToken
    Access token for the Microsoft Graph API.

    .PARAMETER UserFlowId
    The name of the user flow to update the API Connector Configuration.

    .PARAMETER ApiConnectorId
    ID of the API Connector to update the B2C User Flow with.

    .PARAMETER Step
    When (on which step) to add the API Connector.

    .OUTPUTS
    No response.

    .EXAMPLE
    Set-B2CUserFlowApiConnector `
      -AccessToken @{ token_type, access_token } `
      -UserFlowId "B2C_1_SignUpIn" `
      -ApiConnectorId <ID_HERE>
  #>
  param (
    [Parameter(Mandatory = $true)]
    [psobject] $AccessToken,

    [Parameter(Mandatory = $true)]
    [string] $UserFlowId,

    [Parameter(Mandatory = $true)]
    [string] $ApiConnectorId,

    [ValidateSet("postAttributeCollection", "postFederationSignup")]
    [Parameter(Mandatory = $false)]
    [string] $Step = "postAttributeCollection"
  )

  $headers = @{
    "Authorization" = "$($accessToken.token_type) $($accessToken.access_token)"
    "Content-Type"  = "application/json"
  }

  $body = @{
    "@odata.id" = "https://graph.microsoft.com/beta/identity/apiConnectors/$apiConnectorId"
  }

  $response = Invoke-MicrosoftGraphApi `
    -Method "PUT" `
    -Version "beta" `
    -Resource "identity/b2cUserFlows/$UserFlowId/apiConnectorConfiguration/$Step/`$ref" `
    -Headers $headers `
    -Body $body

  return $response
}

Export-ModuleMember -Function Set-B2CUserFlowApiConnector
