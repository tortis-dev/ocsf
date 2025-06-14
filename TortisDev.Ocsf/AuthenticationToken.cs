using System.Text.Json.Serialization;

namespace TortisDev.Ocsf;

/// <summary>
/// Represents an authentication token used in authentication flows
/// </summary>
[PublicAPI]
public class AuthenticationToken
{
    /// <summary>
    /// The unique identifier of the token
    /// </summary>
    [JsonPropertyName("uid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Uid { get; set; }

    /// <summary>
    /// The type of the token (e.g., JWT, OAuth, SAML)
    /// </summary>
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Type { get; set; }

    /// <summary>
    /// The time when the token was issued in Unix timestamp format
    /// </summary>
    [JsonPropertyName("issued_time")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? IssuedTime { get; set; }

    /// <summary>
    /// The time when the token expires in Unix timestamp format
    /// </summary>
    [JsonPropertyName("expiration_time")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? ExpirationTime { get; set; }

    /// <summary>
    /// The issuer of the token
    /// </summary>
    [JsonPropertyName("issuer")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Issuer { get; set; }

    /// <summary>
    /// The subject of the token
    /// </summary>
    [JsonPropertyName("subject")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Subject { get; set; }

    /// <summary>
    /// The audience for which the token is intended
    /// </summary>
    [JsonPropertyName("audience")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Audience { get; set; }
}
