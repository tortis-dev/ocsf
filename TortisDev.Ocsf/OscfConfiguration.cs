using System.Reflection;

namespace TortisDev.Ocsf;

/// <summary>
/// Global configuration for the OCSF library.
/// </summary>
public static class OscfConfiguration
{
    /// <summary>
    /// The product that is reporting the events.
    /// </summary>
    public static Product Product { get; set; } = new Product
    {
        Name = Assembly.GetEntryAssembly()?.GetName().Name,
        Version = Assembly.GetEntryAssembly()?.GetName().Version?.ToString()
    };
}