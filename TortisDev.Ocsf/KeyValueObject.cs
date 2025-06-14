using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TortisDev.Ocsf;

/// <summary>
/// A generic object allowing to define a {key:value} pair.
/// </summary>
[PublicAPI]
public class KeyValueObject : IValidatableObject
{
    /// <summary>
    ///  Initializes a new instance of the KeyValueObject class with a single value.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public KeyValueObject(string name, string value)
    {
        Name = name;
        Value = value;
    }

    /// <summary>
    ///  Initializes a new instance of the KeyValueObject class with multiple values.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="values"></param>
    public KeyValueObject(string name, string[] values)
    {
        Name = name;
        Values = values;
    }

    /// <summary>
    /// The name of the key.
    /// </summary>
    [Required]
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// The value associated to the key.
    /// </summary>
    [JsonPropertyName("value")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Value { get; set; }

    /// <summary>
    /// Optional, the values associated to the key.
    /// You can populate this attribute, when you have multiple values for the same key.
    /// </summary>
    [JsonPropertyName("values")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Values { get; set; }

    /// <inheritdoc/>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Value is null && Values is null)
            yield return new ValidationResult("Either value or values must be present");
    }
}