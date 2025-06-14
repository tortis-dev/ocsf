using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TortisDev.Ocsf.Iam;

/// <summary>
/// Privilege change events relate to modifications of access rights or permissions assigned to users or groups.
/// </summary>
[PublicAPI]
public class UserAccessManagementEvent : IamEvent<UserAccessManagementEvent, UserAccessManagementEvent.Activity>, IValidatableObject
{
    /// <summary>
    /// User management activites.
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
        ///  Assigning privileges to a user.
        /// </summary>
        public static readonly Activity AssignPrivileges = new(1, "Assign Privileges");
        /// <summary>
        ///  Revoking privileges from a user.
        /// </summary>
        public static readonly Activity RevokePrivileges = new(2, "Revoke Privileges");
        /// <summary>
        /// The event activity is not mapped. See the activity_name attribute, which contains a data source specific value.
        /// </summary>
        /// <param name="name">The data source specific value.</param>
        public static Activity Other(string name) => new(99, name);
    }

    /// <summary>
    /// Initializes a new instance of the PrivilegeChange class.
    /// </summary>
    public UserAccessManagementEvent(User user, Activity activity) : base(activity)
    {
        User = user;
        ClassId = 3005;
        ClassName = "User Access Management";
    }

    /// <summary>
    /// The user whose privileges are being modified
    /// </summary>
    [JsonPropertyName("user")]
    public User User { get; set; }

    /// <summary>
    /// The resource for which privileges are being modified
    /// </summary>
    [JsonPropertyName("resources")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Resource[]? Resources { get; set; }

    /// <summary>
    /// The permission being granted or revoked
    /// </summary>
    [JsonPropertyName("privileges")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Privileges { get; set; }

    /// <inheritdoc />
    public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var baseResults = base.Validate(validationContext);

        foreach (var result in baseResults)
        {
            yield return result;
        }

        if (User == null)
        {
            yield return new ValidationResult("User is required.", new[] { nameof(User) });
        }
        else
        {
            foreach (var result in User.Validate(validationContext))
            {
                yield return result;
            }
        }

        if (Resources != null)
        {
            foreach (var resource in Resources)
            {
                foreach (var result in resource.Validate(validationContext))
                {
                    yield return result;
                }
            }
        }
    }
}
