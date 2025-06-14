using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TortisDev.Ocsf;

/// <summary>
/// The Actor object contains details about the user, role, application, service, or process that initiated or
/// performed a specific activity.
/// Note that Actor is not the threat actor of a campaign but may be part of a campaign.
/// </summary>
[PublicAPI]
public class Actor : IValidatableObject
{
    /// <summary>
    /// The client application or service that initiated the activity.
    /// This can be in conjunction with the user if present.
    /// Note that app_name is distinct from the process if present.
    /// </summary>
    [JsonPropertyName("app_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ApplicationName { get; set; }

    /// <summary>
    /// The unique identifier of the client application or service that initiated the activity.
    /// This can be in conjunction with the user if present.
    /// Note that app_uid is distinct from the process.pid or process.uid if present.
    /// </summary>
    [JsonPropertyName("app_uid")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ApplicationId { get; set; }

    /// <summary>
    /// The user that initiated the activity or the user context from which the activity was initiated.
    /// </summary>
    [JsonPropertyName("user")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public User? User { get; set; }

    ///<inheritDoc />
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // At least one of these attributes must be present: app_name, app_uid, invoked_by, process, session, user
        if (string.IsNullOrWhiteSpace(ApplicationName) && string.IsNullOrWhiteSpace(ApplicationId) && User is null)
            yield return new ValidationResult(
                "At least one of these attributes must be present: app_name, app_uid, invoked_by, process, session, user",
                [nameof(ApplicationName), nameof(ApplicationId), nameof(User)]);
    }
}