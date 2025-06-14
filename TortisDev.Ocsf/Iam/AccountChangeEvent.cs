using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TortisDev.Ocsf.Iam;

/// <summary>
/// Account change events relate to the creation, modification, or deletion of user accounts.
/// </summary>
[PublicAPI]
public class AccountChangeEvent : IamEvent<AccountChangeEvent, AccountChangeEvent.Activity>
{
    /// <summary>
    /// Defines activities for the Account Change Event.
    /// </summary>
    public class Activity : IActivity
    {
        /// <summary>
        /// The event activity is unknown.
        /// </summary>
        public static readonly Activity Unknown = new(0, "Unknown");

        /// <summary>
        /// A user/role was created.
        /// </summary>
        public static readonly Activity Create = new(1, "Create");

        /// <summary>
        /// A user/role was enabled.
        /// </summary>
        public static readonly Activity Enable = new(2, "Enable");

        /// <summary>
        /// An attempt was made to change an account's password.
        /// </summary>
        public static readonly Activity PasswordChange = new(3, "Password Change");

        /// <summary>
        /// An attempt was made to reset an account's password.
        /// </summary>
        public static readonly Activity PasswordReset = new(4, "Password Reset");

        /// <summary>
        /// A user/role was disabled.
        /// </summary>
        public static readonly Activity Disable = new(5, "Disable");

        /// <summary>
        /// A user/role was deleted.
        /// </summary>
        public static readonly Activity Delete = new(6, "Delete");

        /// <summary>
        /// An IAM Policy was attached to a user/role.
        /// </summary>
        public static readonly Activity AttachPolicy = new(7, "Attach Policy");

        /// <summary>
        /// An IAM Policy was detached from a user/role.
        /// </summary>
        public static readonly Activity DetachPolicy = new(8, "Detach Policy");

        /// <summary>
        /// A user account was locked out.
        /// </summary>
        public static readonly Activity Lock = new(9, "Lock");

        /// <summary>
        /// An authentication factor was enabled for an account.
        /// </summary>
        public static readonly Activity MfaFactorEnable = new(10, "MFA Factor Enable");

        /// <summary>
        /// An authentication factor was disabled for an account.
        /// </summary>
        public static readonly Activity MfaFactorDisable = new(11, "MFA Factor Disable");

        /// <summary>
        /// A user account was unlocked.
        /// </summary>
        public static readonly Activity Unlock = new(12, "Unlock");

        /// <summary>
        /// The event activity is not mapped. Set the name to the data source specific value.
        /// </summary>
        /// <param name="name">The data source specific value for this activity.</param>
        public static Activity Other(string name) => new(99, name);

        ///<inheritdoc />
        public int Id { get; }
        ///<inheritdoc />
        public string Name { get; }
        private Activity(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    /// <summary>
    /// Initializes a new instance of the AccountChange class.
    /// </summary>
    public AccountChangeEvent(User user, Activity activity) : base(activity)
    {
        User = user;
        ClassId = 3001;
        ClassName = "Account Change";
    }

    /// <summary>
    /// The user account that is being modified
    /// </summary>
    [Required]
    [JsonPropertyName("user")]
    public User User { get; set; }

    /// <inheritdoc/>
    public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var baseResults = base.Validate(validationContext);

        foreach (var result in baseResults)
        {
            yield return result;
        }

        if (User is null)
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
    }

}
