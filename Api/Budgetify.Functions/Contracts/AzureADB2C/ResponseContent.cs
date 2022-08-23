namespace Budgetify.Functions.Contracts.AzureADB2C;

using Newtonsoft.Json;

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

    [JsonProperty("action")]
    public string? Action { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "userMessage")]
    public string? UserMessage { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "status")]
    public string? Status { get; set; }
}
