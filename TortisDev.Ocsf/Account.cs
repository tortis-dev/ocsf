namespace TortisDev.Ocsf;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

/// <summary>
/// The Account object contains the characteristics of an account.
/// </summary>
[PublicAPI]
public class Account : IValidatableObject
{
    /// <summary>
    /// Defines the account types.
    /// </summary>
    [PublicAPI]
    public class AccountType
    {
        /// <summary>
        /// The account type identifier
        /// </summary>
        public int Id { get; }
        
        /// <summary>
        /// The account type, for example: 'System', 'Guest', 'Admin', 'Root', etc.
        /// </summary>
        public string Name { get; }
        
        private AccountType(int id, string name)
        {
            Name = name;
            Id = id;
        }
        
        /// <summary>
        /// The account type is unknown.
        /// </summary>
        public static readonly AccountType Unknown = new (0, "Unknown");
        ///<exclude />
        public static readonly AccountType LdapAccount = new (1, "LDAP Account");
        ///<exclude />
        public static readonly AccountType WindowsAccount = new (2, "Windows Account");
        ///<exclude />
        public static readonly AccountType AwsIamUser = new (3, "AWS IAM User");
        ///<exclude />
        public static readonly AccountType AwsIamRole = new (4, "AWS IAM Role");
        ///<exclude />
        public static readonly AccountType GcpAccount = new (5, "GCP Account");
        ///<exclude />
        public static readonly AccountType AzureAdAccount = new (6, "Azure AD Account");
        ///<exclude />
        public static readonly AccountType MacOsAccount = new (7, "MacOS Account");
        ///<exclude />
        public static readonly AccountType AppleAccount = new (8, "Apple Account");
        ///<exclude />
        public static readonly AccountType LinuxAccount = new (9, "Linux Account");
        ///<exclude />
        public static readonly AccountType AwsAccount = new (10, "AWS Account");
        ///<exclude />
        public static readonly AccountType GcpProject = new (11, "GCP Project");
        ///<exclude />
        public static readonly AccountType OciCompartment = new (12, "OCI Compartment");
        ///<exclude />
        public static readonly AccountType AzureSubscription = new (13, "Azure Subscription");
        ///<exclude />
        public static readonly AccountType SalesforceAccount = new (14, "Salesforce Account");
        ///<exclude />
        public static readonly AccountType GoogleWorkspace = new (15, "Google Workspace");
        ///<exclude />
        public static readonly AccountType ServicenowInstance = new (16, "Servicenow Instance");
        ///<exclude />
        public static readonly AccountType M365Tenant = new (17, "M365 Tenant");
        ///<exclude />
        public static readonly AccountType EmailAccount = new (18, "Email Account");
        /// <summary>
        /// The account type is not mapped. The value is provided by the data source.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static AccountType Other(string name) => new (99, name);
    }

    /// <summary>
    /// Sets the type of the account.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public Account OfType(AccountType type)
    {
        TypeId = type.Id;
        Type = type.Name;
        return this;
    }
    
    /// <summary>
    /// The unique identifier of the account
    /// (e.g., AWS Account ID, OCID, GCP Project ID, Azure Subscription ID, Google Workspace Customer ID, or M365 Tenant UID).
    /// </summary>
    [JsonPropertyName("uid")]
    public string? Uid { get; set; }

    /// <summary>
    /// The name of the account
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// The account type identifier
    /// </summary>
    /// <see cref="AccountType"/>
    [JsonPropertyName("type_id")]
    public int? TypeId { get; private set; }

    /// <summary>
    /// The account type, for example: 'System', 'Guest', 'Admin', 'Root', etc.
    /// </summary>
    /// <see cref="AccountType"/>
    [JsonPropertyName("type")]
    public string? Type { get; private set; }

    /// <summary>
    /// The list of labels associated to the account.
    /// </summary>
    public string[]? Labels { get; set; }
    
    /// <summary>
    /// The list of tags; {key:value} pairs associated to the account.
    /// </summary>
    public KeyValueObject[]? Tags { get; set; }

    #region Windows Extensions
    /// <summary>
    /// The security identifier (SID) of the account (Windows extension)
    /// </summary>
    [JsonPropertyName("sid")]
    public string? Sid { get; set; }

    /// <summary>
    /// The Security Account Manager (SAM) account name (Windows extension)
    /// </summary>
    [JsonPropertyName("sam_account_name")]
    public string? SamAccountName { get; set; }
    
    /// <summary>
    /// The Security Identifier (SID) of the primary group (Windows extension)
    /// </summary>
    [JsonPropertyName("primary_group_sid")]
    public string? PrimaryGroupSid { get; set; }

    /// <summary>
    /// The password last set time in Unix timestamp format (Windows extension)
    /// </summary>
    [JsonPropertyName("password_last_set_time")]
    public long? PasswordLastSetTime { get; set; }
    
    /// <summary>
    /// The account flags (Windows extension)
    /// </summary>
    [JsonPropertyName("flags")]
    public string[]? Flags { get; set; }
    #endregion

    #region Linux Extensions
    /// <summary>
    /// The home directory of the account (Linux extension)
    /// </summary>
    [JsonPropertyName("home_directory")]
    public string? HomeDirectory { get; set; }

    /// <summary>
    /// The login shell of the account (Linux extension)
    /// </summary>
    [JsonPropertyName("shell")]
    public string? Shell { get; set; }

    /// <summary>
    /// The user ID (UID) number of the account (Linux extension)
    /// </summary>
    [JsonPropertyName("uid_number")]
    public int? UidNumber { get; set; }

    /// <summary>
    /// The group ID (GID) number of the account (Linux extension)
    /// </summary>
    [JsonPropertyName("gid_number")]
    public int? GidNumber { get; set; }
    #endregion
    
    /// <inheritdoc />
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Uid) && string.IsNullOrWhiteSpace(Name))
            yield return new ValidationResult("Either uid or name must be present"); 
    }
}
