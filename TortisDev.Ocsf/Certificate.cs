using System.Text.Json.Serialization;

namespace TortisDev.Ocsf;

/// <summary>
/// Represents a digital certificate used for authentication or encryption
/// </summary>
[PublicAPI]
public class Certificate
{
    /// <summary>
    /// The certificate serial number
    /// </summary>
    [JsonPropertyName("serial_number")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? SerialNumber { get; set; }

    /// <summary>
    /// The certificate subject
    /// </summary>
    [JsonPropertyName("subject")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Subject { get; set; }

    /// <summary>
    /// The certificate issuer
    /// </summary>
    [JsonPropertyName("issuer")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Issuer { get; set; }

    /// <summary>
    /// The certificate validity start time in Unix timestamp format
    /// </summary>
    [JsonPropertyName("valid_from")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? ValidFrom { get; set; }

    /// <summary>
    /// The certificate validity end time in Unix timestamp format
    /// </summary>
    [JsonPropertyName("valid_to")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? ValidTo { get; set; }

    /// <summary>
    /// The certificate thumbprint/fingerprint
    /// </summary>
    [JsonPropertyName("thumbprint")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Thumbprint { get; set; }

    /// <summary>
    /// The public key algorithm used in the certificate
    /// </summary>
    [JsonPropertyName("algorithm")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Algorithm { get; set; }
}
