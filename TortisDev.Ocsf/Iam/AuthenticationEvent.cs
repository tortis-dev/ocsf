using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TortisDev.Ocsf.Iam;

/// <summary>
/// Authentication events relate to the validation of identities and credentials of users,
/// devices, processes, or other system entities.
/// </summary>
[PublicAPI]
public class AuthenticationEvent : IamEvent<AuthenticationEvent, AuthenticationEvent.Activity>
{
    /// <summary>
    /// Defines activities for the Event.
    /// </summary>
    public class Activity : IActivity
    {
        /// <summary>
        /// The event activity is unknown.
        /// </summary>
        public static readonly Activity Unknown = new(0, "Unknown");

        /// <summary>
        /// A new logon session was requested.
        /// </summary>
        public static readonly Activity Logon = new(1, "Logon");

        /// <summary>
        /// A logon session was terminated and no longer exists.
        /// </summary>
        public static readonly Activity Logoff = new(2, "Logoff");

        /// <summary>
        /// A Kerberos authentication ticket (TGT) was requested.
        /// </summary>
        public static readonly Activity AuthenticationTicket = new(3, "Authentication Ticket");

        /// <summary>
        /// A Kerberos service ticket was requested.
        /// </summary>
        public static readonly Activity ServiceTicketRequest = new(4, "Service Ticket Request");

        /// <summary>
        /// A Kerberos service ticket was renewed.
        /// </summary>
        public static readonly Activity ServiceTicketRenew = new(5, "Service Ticket Renew");

        /// <summary>
        /// A preauthentication stage was engaged.
        /// </summary>
        public static readonly Activity Preauth = new(6, "Preauth");

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
    /// The logon type, normalized to the caption of the logon_type_id value. In the case of 'Other', it is defined by the event source.
    /// </summary>
    public class LogonType
    {
        /// <summary>
        ///  The logon type identifier.
        /// </summary>
        public int Id { get; }
        /// <summary>
        ///  The logon type, for example: 'Interactive', 'Network', 'Remote Interactive', etc.
        /// </summary>
        public string Name { get; }
        private LogonType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        /// <summary>
        /// The logon type is unknown.
        /// </summary>
        public static readonly LogonType Unknown = new(0, "Unknown");

        /// <summary>
        /// Used only by the System account, for example at system startup.
        /// </summary>
        public static readonly LogonType System = new(1, "System");

        /// <summary>
        /// A local logon to device console.
        /// </summary>
        public static readonly LogonType Interactive = new(2, "Interactive");

        /// <summary>
        /// A user or device logged onto this device from the network.
        /// </summary>
        public static readonly LogonType Network = new(3, "Network");

        /// <summary>
        /// A batch server logon, where processes may be executing on behalf of a user without their direct intervention.
        /// </summary>
        public static readonly LogonType Batch = new(4, "Batch");

        /// <summary>
        /// A logon by a service or daemon that was started by the OS.
        /// </summary>
        public static readonly LogonType OsService = new(5, "OS Service");

        /// <summary>
        /// A user unlocked the device.
        /// </summary>
        public static readonly LogonType Unlock = new(7, "Unlock");

        /// <summary>
        /// A user logged on to this device from the network. The user's password in the authentication package was not hashed.
        /// </summary>
        public static readonly LogonType NetworkCleartext = new(8, "Network Cleartext");

        /// <summary>
        /// A caller cloned its current token and specified new credentials for outbound connections. The new logon session has the same local identity, but uses different credentials for other network connections.
        /// </summary>
        public static readonly LogonType NewCredentials = new(9, "New Credentials");

        /// <summary>
        /// A remote logon using Terminal Services or remote desktop application.
        /// </summary>
        public static readonly LogonType RemoteInteractive = new(10, "Remote Interactive");

        /// <summary>
        /// A user logged on to this device with network credentials that were stored locally on the device and the domain controller was not contacted to verify the credentials.
        /// </summary>
        public static readonly LogonType CachedInteractive = new(11, "Cached Interactive");

        /// <summary>
        /// Same as Remote Interactive. This is used for internal auditing.
        /// </summary>
        public static readonly LogonType CachedRemoteInteractive = new(12, "Cached Remote Interactive");

        /// <summary>
        /// Workstation logon.
        /// </summary>
        public static readonly LogonType CachedUnlock = new(13, "Cached Unlock");

        /// <summary>
        /// The logon type is not mapped. See the logon_type attribute, which contains a data source specific value.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static LogonType Other(string name) => new(99, name);
    }

    /// <summary>
    /// Sets the logon type for this event.
    /// </summary>
    /// <param name="logonType"></param>
    /// <returns></returns>
    public AuthenticationEvent UsingLogonType(LogonType logonType)
    {
        LogonTypeName = logonType.Name;
        LogonTypeId = logonType.Id;
        return this;
    }

    /// <summary>
    /// Initializes a new instance of the Authentication class.
    /// </summary>
    public AuthenticationEvent(User user, Activity activity) : base(activity)
    {
        User = user;
        ClassId = 3002;
        ClassName = "Authentication";
        LogonTypeId = LogonType.Unknown.Id;
        LogonTypeName = LogonType.Unknown.Name;
    }

    /// <summary>
    /// The logon type or authentication protocol used
    /// </summary>
    [JsonPropertyName("logon_type")]
    public string LogonTypeName { get; set; }

    /// <summary>
    /// The integer code for the logon type
    /// </summary>
    [JsonPropertyName("logon_type_id")]
    public int? LogonTypeId { get; set; }

    /// <summary>
    /// The authenticated user information
    /// </summary>
    [JsonPropertyName("user")]
    public User User { get; set; }

    /// <summary>
    /// The duration of the session in seconds
    /// </summary>
    [JsonPropertyName("session_duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? SessionDuration { get; set; }

    /// <summary>
    /// Indicates if authentication used multi-factor authentication
    /// </summary>
    [JsonPropertyName("is_mfa")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsMfa { get; set; }

    /// <summary>
    /// Indicates if this was a new logon session
    /// </summary>
    [JsonPropertyName("is_new_logon")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsNewLogon { get; set; }

    /// <summary>
    /// Indicates if the logon was remote
    /// </summary>
    [JsonPropertyName("is_remote")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsRemote { get; set; }

    /// <summary>
    /// The protocol used for authentication
    /// </summary>
    [JsonPropertyName("auth_protocol")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AuthProtocol { get; set; }

    /// <summary>
    /// The integer code for the authentication protocol
    /// </summary>
    [JsonPropertyName("auth_protocol_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? AuthProtocolId { get; set; }

    /// <summary>
    /// The list of authentication factors used
    /// </summary>
    [JsonPropertyName("auth_factors")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<AuthFactor>? AuthFactors { get; set; }

    /// <summary>
    /// The session information
    /// </summary>
    [JsonPropertyName("session")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Session? Session { get; set; }

    /// <summary>
    /// The source endpoint of the authentication event
    /// </summary>
    [JsonPropertyName("src_endpoint")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public NetworkEndpoint? SourceEndpoint { get; set; }

    /// <summary>
    /// The destination endpoint of the authentication event
    /// </summary>
    [JsonPropertyName("dst_endpoint")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public NetworkEndpoint? DestinationEndpoint { get; set; }

    /// <summary>
    /// Indicates if credentials were transmitted in cleartext
    /// </summary>
    [JsonPropertyName("is_cleartext")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IsCleartext { get; set; }

    /// <summary>
    /// Authentication token information
    /// </summary>
    [JsonPropertyName("authentication_token")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public AuthenticationToken? AuthenticationToken { get; set; }

    /// <summary>
    /// The process responsible for the logon
    /// </summary>
    [JsonPropertyName("logon_process")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Process? LogonProcess { get; set; }

    /// <summary>
    /// Certificate used for authentication
    /// </summary>
    [JsonPropertyName("certificate")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Certificate? Certificate { get; set; }
}
