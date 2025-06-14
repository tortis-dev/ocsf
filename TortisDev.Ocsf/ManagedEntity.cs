namespace TortisDev.Ocsf;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

/// <summary>
/// The Managed Entity object describes the type and version of an entity, such as a user, device, or policy.
/// For types in the type_id enum list, an associated attribute should be populated. If the type of entity is not in
/// the type_id list, information can be put into the data attribute, type_id should be 'Other' and the type
/// attribute should label the entity type.
/// </summary>
public class ManagedEntity : IValidatableObject
{
    /// <summary>
    /// Entity types that can be used to classify managed entities such as users, devices, policies, etc.
    /// </summary>
    public class EntityType
    {
        /// <summary>
        /// The identifier of the entity type.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The name of the entity type.
        /// </summary>
        public string Name { get; }

        private EntityType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// The type is unknown.
        /// </summary>
        public static readonly EntityType Unknown = new(0, "Unknown");

        /// <summary>
        /// A managed Device entity. This item corresponds to population of the device attribute.
        /// </summary>
        public static readonly EntityType Device = new(1, "Device");

        /// <summary>
        /// A managed User entity. This item corresponds to population of the user attribute.
        /// </summary>
        public static readonly EntityType User = new(2, "User");

        /// <summary>
        /// A managed Group entity. This item corresponds to population of the group attribute.
        /// </summary>
        public static readonly EntityType Group = new(3, "Group");

        /// <summary>
        /// A managed Organization entity. This item corresponds to population of the org attribute.
        /// </summary>
        public static readonly EntityType Organization = new(4, "Organization");

        /// <summary>
        /// A managed Policy entity. This item corresponds to population of the policy attribute.
        /// </summary>
        public static readonly EntityType Policy = new(5, "Policy");

        /// <summary>
        /// A managed Email entity. This item corresponds to population of the email attribute.
        /// </summary>
        public static readonly EntityType Email = new(6, "Email");

        /// <summary>
        /// A managed Network Zone entity. Populate the name attribute with the zone name and/or the uid attribute with the zone ID.
        /// Additional zone information can be added to the data attribute.
        /// </summary>
        public static readonly EntityType NetworkZone = new(7, "Network Zone");

        /// <summary>
        /// The type is not mapped. See the type attribute, which contains a data source specific value.
        /// </summary>
        public static EntityType Other(string name) => new(99, name);
    }

    /// <summary>
    ///  Initializes a new instance of the ManagedEntity class with an unknown type.
    /// </summary>
    /// <returns></returns>
    public static ManagedEntity OfUnknownType()
    {
        return new ManagedEntity
        {
            Type = EntityType.Unknown.Name,
            TypeId = EntityType.Unknown.Id,
            Name = EntityType.Unknown.Name
        };
    }

    /// <summary>
    ///   Initializes a new instance of the ManagedEntity class with a specific type.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static ManagedEntity Other(string name)
    {
        var type = EntityType.Other(name);
        return new ManagedEntity
        {
            Name = name,
            Type = type.Name,
            TypeId = type.Id
        };
    }

    /// <summary>
    ///  Initializes a new instance of the ManagedEntity class with a User;
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static ManagedEntity OfTypeUser(User user)
    {
        return new ManagedEntity
        {
            User = user,
            Uid = user.Uid,
            Name = user.Name,
            Type = EntityType.User.Name,
            TypeId = EntityType.User.Id
        };
    }

    /// <summary>
    ///  Initializes a new instance of the ManagedEntity class with default values.
    /// </summary>
    private ManagedEntity()
    {
        Type = EntityType.Unknown.Name;
        TypeId = EntityType.Unknown.Id;
    }

    /// <summary>
    /// The identifier of the managed entity. It should match the uid of the specific entity's object UID if populated,
    /// or the source specific ID if the type_id is 'Other'.
    /// </summary>
    [JsonPropertyName("uid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Uid { get; private set; }

    /// <summary>
    /// The name of the managed entity. It should match the name of the specific entity object's name if populated,
    /// or the name of the managed entity if the type_id is 'Other'.
    /// </summary>
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Name { get; private set; }

    /// <summary>
    /// The user that pertains to the event or object.
    /// </summary>
    [JsonPropertyName("user")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public User? User { get; private set; }

    /// <summary>
    /// The group object associated with an entity such as user, policy, or rule.
    /// </summary>
    [JsonPropertyName("group")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Group? Group { get; private set; }

    /// <summary>
    /// The version of the managed entity. For example: 1.2.3.
    /// </summary>
    [JsonPropertyName("version")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Version { get; set; }

    /// <summary>
    /// The managed entity type. For example: Policy, User, Organization, Device.
    /// </summary>
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Type { get; private set; }

    /// <summary>
    /// The type of the Managed Entity. It is recommended to also populate the type attribute with the
    /// associated label, or the source specific name if Other.
    /// </summary>
    [JsonPropertyName("type_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int TypeId { get; private set; }

    /// <summary>
    /// The managed entity content as a object. The object must be serializable to JSON.
    /// </summary>
    [JsonPropertyName("data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Data { get; set; }


    /// <inheritdoc/>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Uid is null && Name is null && User is null && Group is null)
        {
            yield return new ValidationResult("Either Uid, Name, User, or Group must be provided.", new[] { nameof(Uid), nameof(Name), nameof(User), nameof(Group) });
        }
    }
}
