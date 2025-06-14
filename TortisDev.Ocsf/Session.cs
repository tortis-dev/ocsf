using System.Text.Json.Serialization;

namespace TortisDev.Ocsf;

/// <summary>
/// Represents a user session
/// </summary>
[PublicAPI]
public class Session
{
    /// <summary>
    /// The unique identifier for the session
    /// </summary>
    [JsonPropertyName("uid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Uid { get; set; }

    /// <summary>
    /// The start time of the session in Unix timestamp format
    /// </summary>
    [JsonPropertyName("start_time")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? StartTime { get; set; }

    /// <summary>
    /// The end time of the session in Unix timestamp format
    /// </summary>
    [JsonPropertyName("end_time")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? EndTime { get; set; }

    /// <summary>
    /// The duration of the session in seconds
    /// </summary>
    [JsonPropertyName("duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? Duration { get; set; }

    /// <summary>
    /// The user associated with the session
    /// </summary>
    [JsonPropertyName("user")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public User? User { get; set; }

    /// <summary>
    /// Indicates whether the session is currently active
    /// </summary>
    [JsonPropertyName("is_active")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsActive { get; set; }

    /// <summary>
    /// The type of the session
    /// </summary>
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Type { get; set; }
}
