namespace TortisDev.Ocsf;

/// <summary>
/// The normalized identifier of the event status.
/// </summary>
[PublicAPI]
public class Status
{
    /// <summary>
    /// The status_id
    /// </summary>
    public int Id { get; }
    
    /// <summary>
    /// The status_name
    /// </summary>
    public string Name { get; }
    
    private Status(int statusId, string status)
    {
        Id = statusId;
        Name = status;
    }
    
    /// <summary>
    /// The status is unknown.
    /// </summary>
    public static readonly Status Unknown = new(0, "Unknown");
    
    /// <summary>
    /// The event represents a successful operation. e.g. a successful logon attempt.
    /// </summary>
    public static readonly Status Success = new(1, "Success");
    
    /// <summary>
    /// The event represents a failed operation. e.g. a failed logon attempt.
    /// </summary>
    public static readonly Status Failure = new(2, "Failure");
    
    /// <summary>
    /// The event status is not mapped. See the status attribute, which contains a data source specific value.
    /// </summary>
    public static Status Other(string name) => new(99, name);
}