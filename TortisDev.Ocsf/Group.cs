using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace TortisDev.Ocsf;

/// <summary>
/// Represents a group in the identity management system
/// </summary>
public class Group : IValidatableObject
{
    /// <summary>
    /// The unique identifier for the group
    /// </summary>
    [MaybeNull]
    [JsonPropertyName("uid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Uid { get; set; }

    /// <summary>
    /// The name of the group
    /// </summary>
    [MaybeNull]
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Name { get; set; }

    /// <summary>
    /// The type of the group
    /// </summary>
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Type { get; set; }

    /// <summary>
    /// The domain to which the group belongs
    /// </summary>
    [JsonPropertyName("domain")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Domain { get; set; }

    /// <summary>
    /// Additional privileges of the group
    /// </summary>
    [JsonPropertyName("privileges")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Privileges { get; set; }
    
    /// <summary>
    /// The group description.
    /// </summary>
    [JsonPropertyName("desc")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; set; }

    ///<inheritDoc />
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Uid == null && Name == null)
            yield return new ValidationResult("Either uid or name must be present");
    }
}
