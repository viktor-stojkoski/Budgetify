namespace Budgetify.Contracts.User.Requests
{

    using Newtonsoft.Json;

    public class CreateUserRequest
    {
        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("givenName")]
        public string? FirstName { get; set; }

        [JsonProperty("surname")]
        public string? LastName { get; set; }

        [JsonProperty("city")]
        public string? City { get; set; }
    }
}
