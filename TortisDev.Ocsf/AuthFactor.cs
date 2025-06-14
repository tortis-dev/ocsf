using System.Text.Json.Serialization;

namespace TortisDev.Ocsf;

/// <summary>
/// Represents an authentication factor used in authentication processes
/// </summary>
[PublicAPI]
public class AuthFactor
{
    /// <summary>
    /// Defines the types of authentication factors
    /// </summary>
    public class FactorType
    {
        /// <summary>
        /// The factor type identifier
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The name of the factor type
        /// </summary>
        public string Name { get; }

        private FactorType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// Unknown factor type
        /// </summary>
        public static readonly FactorType Unknown = new(0, "Unknown");

        /// <summary>
        /// Something the user knows (password, PIN, etc.)
        /// </summary>
        public static readonly FactorType Knowledge = new(1, "Knowledge");

        /// <summary>
        /// Something the user has (smart card, security token, etc.)
        /// </summary>
        public static readonly FactorType Possession = new(2, "Possession");

        /// <summary>
        /// Something the user is (fingerprint, facial recognition, etc.)
        /// </summary>
        public static readonly FactorType Inherence = new(3, "Inherence");

        /// <summary>
        /// Location-based authentication
        /// </summary>
        public static readonly FactorType Location = new(4, "Location");

        /// <summary>
        /// Other factor type not in the predefined list
        /// </summary>
        public static FactorType Other(string name) => new(99, name);
    }

    /// <summary>
    /// Sets the type of the authentication factor
    /// </summary>
    public AuthFactor OfType(FactorType type)
    {
        TypeId = type.Id;
        Type = type.Name;
        return this;
    }

    /// <summary>
    /// The factor type identifier
    /// </summary>
    [JsonPropertyName("type_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? TypeId { get; private set; }

    /// <summary>
    /// The name of the factor type
    /// </summary>
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Type { get; private set; }

    /// <summary>
    /// The name of the specific authentication factor
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; set; }

    /// <summary>
    /// The method used to verify this factor
    /// </summary>
    [JsonPropertyName("verification_method")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? VerificationMethod { get; set; }

    /// <summary>
    /// Whether the factor was successfully verified
    /// </summary>
    [JsonPropertyName("is_verified")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsVerified { get; set; }
}
