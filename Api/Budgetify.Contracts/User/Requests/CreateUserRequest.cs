namespace Budgetify.Contracts.User.Requests
{
    using System.Text.Json.Serialization;

    //using Newtonsoft.Json;

    public class CreateUserRequest
    {
        [JsonPropertyName("givenName")]
        public string? FirstName { get; set; }

        [JsonPropertyName("surname")]
        public string? LastName { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }
    }
}
