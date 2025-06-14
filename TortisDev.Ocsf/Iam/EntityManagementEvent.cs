using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace TortisDev.Ocsf.Iam;

/// <summary>
/// Entity Management events report activity by a managed client, a micro service, or a user at a management console.
/// The activity can be a create, read, update, and delete operation on a managed entity.
/// </summary>
[PublicAPI]
public class EntityManagementEvent : IamEvent<EntityManagementEvent, EntityManagementEvent.Activity>
{
    /// <summary>
    ///  Defines activities for the Entity Management Event.
    /// </summary>
    public class Activity : IActivity
    {
        /// <inheritdoc/>
        public int Id { get; }
        /// <inheritdoc/>
        public string Name { get; }

        private Activity(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// The event activity is unknown.
        /// </summary>
        public static readonly Activity Unknown = new(0, "Unknown");

        /// <summary>
        /// Create a new managed entity.
        /// </summary>
        public static readonly Activity Create = new(1, "Create");

        /// <summary>
        /// Read an existing managed entity.
        /// </summary>
        public static readonly Activity Read = new(2, "Read");

        /// <summary>
        /// Update an existing managed entity.
        /// </summary>
        public static readonly Activity Update = new(3, "Update");

        /// <summary>
        /// Delete a managed entity.
        /// </summary>
        public static readonly Activity Delete = new(4, "Delete");

        /// <summary>
        /// Move or rename an existing managed entity.
        /// </summary>
        public static readonly Activity Move = new(5, "Move");

        /// <summary>
        /// Enroll an existing managed entity.
        /// </summary>
        public static readonly Activity Enroll = new(6, "Enroll");

        /// <summary>
        /// Unenroll an existing managed entity.
        /// </summary>
        public static readonly Activity Unenroll = new(7, "Unenroll");

        /// <summary>
        /// Enable an existing managed entity. Note: This is typically regarded as a semi-permanent, editor visible, syncable change.
        /// </summary>
        public static readonly Activity Enable = new(8, "Enable");

        /// <summary>
        /// Disable an existing managed entity. Note: This is typically regarded as a semi-permanent, editor visible, syncable change.
        /// </summary>
        public static readonly Activity Disable = new(9, "Disable");

        /// <summary>
        /// Activate an existing managed entity. Note: This is a typically regarded as a transient change, a change of state of the engine.
        /// </summary>
        public static readonly Activity Activate = new(10, "Activate");

        /// <summary>
        /// Deactivate an existing managed entity. Note: This is a typically regarded as a transient change, a change of state of the engine.
        /// </summary>
        public static readonly Activity Deactivate = new(11, "Deactivate");

        /// <summary>
        /// Suspend an existing managed entity.
        /// </summary>
        public static readonly Activity Suspend = new(12, "Suspend");

        /// <summary>
        /// Resume (unsuspend) an existing managed entity.
        /// </summary>
        public static readonly Activity Resume = new(13, "Resume");
        /// <summary>
        /// The event activity is not mapped. See the activity_name attribute, which contains a data source specific value.
        /// </summary>
        /// <param name="name">A data source specific value</param>
        public static Activity Other(string name) => new(99, name);
    }

    /// <summary>
    /// Initializes a new instance of the CredentialChange class.
    /// </summary>
    public EntityManagementEvent(ManagedEntity entity, Activity activity) : base(activity)
    {
        Entity = entity;
        ClassId = 3004;
        ClassName = "Entity Management";
    }

    /// <summary>
    /// The user provided comment about why the entity was changed.
    /// </summary>
    [JsonPropertyName("comment")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Comment { get; set; }

    /// <summary>
    /// The managed entity that is being acted upon.
    /// </summary>
    [JsonPropertyName("entity")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ManagedEntity Entity { get; }


    /// <inheritdoc/>
    public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var baseResults = base.Validate(validationContext);
        foreach (var result in baseResults)
        {
            yield return result;
        }

        var entityResults = Entity.Validate(validationContext);
        foreach (var result in entityResults)
        {
            yield return result;
        }
    }
}