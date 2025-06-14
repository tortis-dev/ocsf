using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TortisDev.Ocsf.Iam;

/// <summary>
/// Authorization events relate to the verification of access rights for a user, device, or system entity.
/// </summary>
[PublicAPI]
public class AuthorizeSessionEvent : IamEvent<AuthorizeSessionEvent, AuthorizeSessionEvent.Activity>
{
    /// <summary>
    /// The normalized identifier of the activity that triggered the event.
    /// </summary>
    [PublicAPI]
    public class Activity : IActivity
    {
        ///<inheritdoc />
        public int Id { get; }
        ///<inheritdoc />
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
        /// Assign special privileges to a new logon.
        /// </summary>
        public static readonly Activity AssignPrivileges = new(0, "Assign Privileges");

        /// <summary>
        /// Assign special groups to a new logon.
        /// </summary>
        public static readonly Activity AssignGroups = new(0, "Assign Groups");

        /// <summary>
        /// The event activity is not mapped. Set the name to the data source specific value.
        /// </summary>
        /// <param name="name">The data source specific value for this activity.</param>
        public static Activity Other(string name) => new(99, name);
    }

    /// <summary>
    /// Initializes a new instance of the AuthorizationEvent class.
    /// </summary>
    public AuthorizeSessionEvent(User user, Activity activity) : base(activity)
    {
        User = user;
        ClassId = 3003;
        ClassName = "Authorize Session";
    }

    /// <summary>
    /// The user requesting authorization
    /// </summary>
    [JsonPropertyName("user")]
    public User User { get; set; }
}
