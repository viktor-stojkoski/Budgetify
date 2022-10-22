namespace Budgetify.Contracts.User.Requests;

using Newtonsoft.Json;

public class UpdateUserClaimsRequest
{
    [JsonProperty("email")]
    public string? Email { get; set; }
}
