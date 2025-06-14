namespace TortisDev.Ocsf;

/// <summary>
/// The normalized severity is a measurement the effort and expense required to manage and resolve an event or
/// incident. Smaller numerical values represent lower impact events, and larger numerical values represent
/// higher impact events.
/// </summary>
[PublicAPI]
public class Severity
{
    /// <summary>
    /// The Severity identifier
    /// </summary>
    public int Id { get; }
    
    /// <summary>
    /// The Severity
    /// </summary>
    public string Name { get; }
    
    private Severity(int id, string name)
    {
        Id = id;
        Name = name;
    }
    
    /// <summary>
    /// The status is unknown.
    /// </summary>
    public static readonly Severity Unknown = new(0, "Unknown");
    
    /// <summary>
    /// Informational message. No action required.
    /// </summary>
    public static readonly Severity Informational = new(1, "Informational");

    /// <summary>
    /// The user decides if action is needed.
    /// </summary>
    public static readonly Severity Low = new(2, "Low");

    /// <summary>
    /// Action is required but the situation is not serious at this time.
    /// </summary>
    public static readonly Severity Medium = new(3, "Medium");

    /// <summary>
    /// Action is required immediately.
    /// </summary>
    public static readonly Severity High = new(4, "High");

    /// <summary>
    /// Action is required immediately and the scope is broad.
    /// </summary>
    public static readonly Severity Critical = new(5, "Critical");

    /// <summary>
    /// An error occurred but it is too late to take remedial action.
    /// </summary>
    public static readonly Severity Fatal = new(6, "Fatal");
    
    /// <summary>
    /// The event/finding severity is not mapped. See the severity attribute, which contains a data source specific value.
    /// </summary>
    public static Severity Other(string name) => new(99, name);
}