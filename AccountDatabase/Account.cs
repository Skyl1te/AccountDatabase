using System.Text.Json.Serialization;

namespace AccountDatabase;

public class Account
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }
    
    [JsonPropertyName("password")]
    public string PasswordHash { get; set; }
    
    [JsonPropertyName("registered_at")]
    public DateTime RegistrationTime { get; set; }

    
}
