using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace TortisDev.Ocsf;

/// <summary>
/// The User object describes the characteristics of a user/person or a security principal.
/// </summary>
[PublicAPI]
public class User : IValidatableObject
{
    /// <summary>
    /// Defines user types.
    /// </summary>
    public class UserType
    {
        /// <summary>
        /// The account type identifier.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The account type, for example: 'User', 'Admin', 'System', etc.
        /// </summary>
        public string Name { get; }

        private UserType(int id, string name)
        {
            Name = name;
            Id = id;
        }

        /// <summary>
        /// The type is unknown.
        /// </summary>
        public static readonly UserType Unknown = new (0, "Unknown");
        /// <summary>
        /// Regular user account.
        /// </summary>
        public static readonly UserType User = new (1, "User");
        /// <summary>
        /// Admin/root user account.
        /// </summary>
        public static readonly UserType Admin = new (2, "Admin");
        /// <summary>
        /// System account. For example, Windows computer accounts with a trailing dollar sign ($).
        /// </summary>
        public static readonly UserType System = new (3, "System");
        /// <summary>
        /// The type is not mapped. See the type attribute, which contains a data source specific value.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static UserType Other(string name) => new (99, name);
    }

    /// <summary>
    /// Sets the user type
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public User OfType(UserType type)
    {
        TypeId = type.Id;
        Type = type.Name;
        return this;
    }

    /// <summary>
    /// The unique user identifier. For example, the Windows user SID, ActiveDirectory DN or AWS user ARN.
    /// </summary>
    [MaybeNull]
    [JsonPropertyName("uid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Uid { get; set; }

    /// <summary>
    /// The username. For example, janedoe1.
    /// </summary>
    [MaybeNull]
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Name { get; set; }

    /// <summary>
    /// The username or account name
    /// </summary>
    [MaybeNull]
    [JsonPropertyName("account")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Account Account { get; set; }

    /// <summary>
    /// The type of the user account
    /// </summary>
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Type { get; private set; }

    /// <summary>
    /// The type of the user account
    /// </summary>
    [JsonPropertyName("type_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? TypeId { get; private set; }

    /// <summary>
    /// The domain where the user is defined. For example: the LDAP or Active Directory domain.
    /// </summary>
    [JsonPropertyName("domain")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Domain { get; set; }

    /// <summary>
    /// The full name of the user
    /// </summary>
    [JsonPropertyName("full_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FullName { get; set; }

    /// <summary>
    /// The email address of the user
    /// </summary>
    [JsonPropertyName("email_addr")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Email { get; set; }

    /// <summary>
    /// The groups to which the user belongs
    /// </summary>
    [JsonPropertyName("groups")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Group[]? Groups { get; set; }

    /// <summary>
    /// Additional attributes of the user
    /// </summary>
    [JsonPropertyName("attributes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, object>? Attributes { get; set; }

    ///<inheritdoc />
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Uid) && string.IsNullOrWhiteSpace(Name) && Account is null)
            yield return new ValidationResult("Either uid, name, or account must be present",  new[] { nameof(Uid), nameof(Name), nameof(Account) });

        if (Groups is not null)
        {
            foreach (var group in Groups)
            {
                var results = group.Validate(validationContext);
                foreach (var result in results)
                {
                    yield return result;
                }
            }
        }
    }
}