namespace Budgetify.Functions.Contracts.AzureADB2C;

using Newtonsoft.Json;

/// <summary>
/// Representing Azure AD B2C Response.
/// </summary>
public class ResponseContent
{
    public ResponseContent()
    {
        Action = "Continue";
    }

    public ResponseContent(string? action, string? userMessage)
    {
        Action = action;
        UserMessage = userMessage;

        if (action == "ValidationError")
        {
            Status = "400";
        }
    }

    /// <summary>
    /// Azure AD B2C Response action.
    /// </summary>
    [JsonProperty("action")]
    public string? Action { get; set; }

    /// <summary>
    /// Azure AD B2C Response message.
    /// </summary>
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "userMessage")]
    public string? UserMessage { get; set; }

    /// <summary>
    /// Azure AD B2C Response status.
    /// </summary>
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "status")]
    public string? Status { get; set; }
}
