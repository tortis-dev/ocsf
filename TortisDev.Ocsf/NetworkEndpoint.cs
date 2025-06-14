using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TortisDev.Ocsf;

/// <summary>
/// Represents a network endpoint with IP address, hostname, and port information
/// </summary>
[PublicAPI]
public class NetworkEndpoint : IValidatableObject
{
    /// <summary>
    /// The IP address of the endpoint
    /// </summary>
    [JsonPropertyName("ip")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? IpAddress { get; set; }

    /// <summary>
    /// The hostname of the endpoint
    /// </summary>
    [JsonPropertyName("hostname")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Hostname { get; set; }

    /// <summary>
    /// The port number used by the endpoint
    /// </summary>
    [JsonPropertyName("port")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Port { get; set; }

    /// <summary>
    /// The IP version (4 or 6)
    /// </summary>
    [JsonPropertyName("ip_version")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? IpVersion { get; set; }

    /// <summary>
    /// The MAC address of the endpoint
    /// </summary>
    [JsonPropertyName("mac")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? MacAddress { get; set; }

    /// <summary>
    /// The domain name of the endpoint
    /// </summary>
    [JsonPropertyName("domain")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Domain { get; set; }

    /// <summary>
    /// Validates that at least one identifying property is set
    /// </summary>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(IpAddress) && string.IsNullOrWhiteSpace(Hostname) && string.IsNullOrWhiteSpace(MacAddress))
            yield return new ValidationResult("At least one of IP address, hostname, or MAC address must be provided");
    }
}
