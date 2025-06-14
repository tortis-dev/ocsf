using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TortisDev.Ocsf;

/// <summary>
/// The Product object describes characteristics of a software product.
/// </summary>
[PublicAPI]
public class Product : IValidatableObject
{
    /// <summary>
    /// The unique identifier of the product.
    /// </summary>
    [JsonPropertyName("uid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? UniqueId { get; set; }
    
    /// <summary>
    /// The name of the product.
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }
    
    /// <summary>
    /// The version of the product, as defined by the event source. For example: 2013.1.3-beta.
    /// </summary>
    [JsonPropertyName("version")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Version { get; set; }
    
    /// <summary>
    /// The vendor name of the product.
    /// </summary>
    [JsonPropertyName("vendor_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? VendorName { get; set; }

    ///<inheritdoc />
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (UniqueId == null && Name == null)
            yield return new ValidationResult("Either uid or name must be present");
    }
}