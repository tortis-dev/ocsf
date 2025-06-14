using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TortisDev.Ocsf;

/// <summary>
/// Represents a resource that can be accessed in the system
/// </summary>
[PublicAPI]
public class Resource : IValidatableObject
{
    /// <summary>
    /// The unique identifier for the resource
    /// </summary>
    [JsonPropertyName("uid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Uid { get; set; }

    /// <summary>
    /// The name of the resource
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }

    ///<inheritdoc />
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if(Uid == null && Name == null)
            yield return new ValidationResult("Either uid or name must be present");
    }
}
