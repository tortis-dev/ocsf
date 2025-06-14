using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TortisDev.Ocsf.Iam;

/// <summary>
/// Group management events relate to the creation, modification, or deletion of groups
/// and group memberships.
/// </summary>
[PublicAPI]
public class GroupManagementEvent : IamEvent<GroupManagementEvent, GroupManagementEvent.Activity>
{
    /// <summary>
    ///  Defines activities for the Group Management Event.
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
        /// he event activity is unknown.
        /// </summary>
        public static readonly Activity Unknown = new(0, "Unknown");

        /// <summary>
        /// Assign privileges to a group.
        /// </summary>
        public static readonly Activity AssignPrivileges = new(1, "Assign Privileges");

        /// <summary>
        /// Revoke privileges from a group.
        /// </summary>
        public static readonly Activity RevokePrivileges = new(2, "Revoke Privileges");

        /// <summary>
        /// Add user to a group.
        /// </summary>
        public static readonly Activity AddUser = new(3, "Add User");

        /// <summary>
        /// Remove user from a group.
        /// </summary>
        public static readonly Activity RemoveUser = new(4, "Remove User");

        /// <summary>
        /// A group was deleted.
        /// </summary>
        public static readonly Activity Delete = new(5, "Delete");

        /// <summary>
        /// A group was created.
        /// </summary>
        public static readonly Activity Create = new(6, "Create");
        /// <summary>
        /// The event activity is not mapped. See the activity_name attribute, which contains a data source specific value.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Activity Other(string name) => new(99, name);
    }

    /// <summary>
    /// Initializes a new instance of the GroupManagement class.
    /// </summary>
    public GroupManagementEvent(Group group, Activity activity) : base(activity)
    {
        Group = group;
        ClassId = 3006;
        ClassName = "Group Management";
    }

    /// <summary>
    /// The group that is being modified
    /// </summary>
    [JsonPropertyName("group")]
    public Group Group { get; set; }

    /// <summary>
    /// A user that was added to or removed from the group.
    /// If the activity is not related to a user, this will be null.
    /// </summary>
    [JsonPropertyName("user")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public User? User { get; set; }

    /// <summary>
    /// A list of privileges assigned to the group.
    /// This should used when privileges are assigned or revoked from the group.
    /// </summary>
    [JsonPropertyName("privileges")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? Privileges { get; set; }
}
