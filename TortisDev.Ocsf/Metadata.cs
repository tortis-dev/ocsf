using System.Text.Json.Serialization;

namespace TortisDev.Ocsf;

/// <summary>
/// The Metadata object describes the metadata associated with the event. 
/// </summary>
public class Metadata
{
    /// <summary>
    /// The version of the OCSF schema, using Semantic Versioning Specification (SemVer).
    /// For example: 1.0.0.
    /// dEvent consumers use the version to determine the available event attributes.
    /// </summary>
    [JsonPropertyName("version")]
    public string Version => "1.5.0";
    
    /// <summary>
    /// The list of labels attached to the event. For example: ["sample", "dev"]
    /// </summary>
    [JsonPropertyName("labels")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Labels { get; set; }

    /// <summary>
    /// The product that reported the event.
    /// </summary>
    [JsonPropertyName("product")]
    public Product Product => OscfConfiguration.Product;
}