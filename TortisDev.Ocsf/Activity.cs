namespace TortisDev.Ocsf;

/// <summary>
/// Defines the interface for all activities.
/// </summary>
public interface IActivity
{
    /// <summary>
    /// The id of the activity as defined by the OCSF specification.
    /// </summary>
    int Id { get; }
    
    /// <summary>
    /// The name of the activity as defined by the OCSF specification.
    /// </summary>
    string Name { get; }
}
