using System.Text.Json.Serialization;

namespace TortisDev.Ocsf;

/// <summary>
/// Represents a process within an operating system
/// </summary>
[PublicAPI]
public class Process
{
    /// <summary>
    /// The process identifier
    /// </summary>
    [JsonPropertyName("pid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? ProcessId { get; set; }

    /// <summary>
    /// The name of the process
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }

    /// <summary>
    /// The path to the process executable
    /// </summary>
    [JsonPropertyName("path")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Path { get; set; }

    /// <summary>
    /// The command line used to launch the process
    /// </summary>
    [JsonPropertyName("cmd_line")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CommandLine { get; set; }

    /// <summary>
    /// The user that owns the process
    /// </summary>
    [JsonPropertyName("user")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public User? User { get; set; }

    /// <summary>
    /// The parent process
    /// </summary>
    [JsonPropertyName("parent")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Process? Parent { get; set; }
}
