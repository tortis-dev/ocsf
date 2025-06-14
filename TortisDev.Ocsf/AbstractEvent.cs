namespace TortisDev.Ocsf;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

/// <summary>
/// Base class for all events.
/// This is not part of the OCSF schema but rather provides the common structure and assignment
/// methods for the POCO representations of events and working with this SDK in a fluent manner.
/// </summary>
/// <typeparam name="TEvent"></typeparam>
/// <typeparam name="TActivity"></typeparam>
[PublicAPI]
public abstract class AbstractEvent<TEvent, TActivity> : IValidatableObject
    where TEvent : AbstractEvent<TEvent, TActivity>
    where TActivity : IActivity
{
    /// <summary>
    /// Defaults Status, Severity to Unknown and OccurredAt to the current time in UTC.
    /// </summary>
    protected AbstractEvent(TActivity activity)
    {
        ActivityId = activity.Id;
        ActivityName = activity.Name;
        WithStatus(Ocsf.Status.Unknown);
        WithSeverity(Ocsf.Severity.Unknown);
        OccurredAt(DateTimeOffset.UtcNow);
    }

    /// <summary>
    /// The normalized identifier of the activity that triggered the event.
    /// </summary>
    [JsonPropertyName("activity_id")]
    public int ActivityId { get; private set; }

    /// <summary>
    /// The event activity name, as defined by the activity_id.
    /// </summary>
    [JsonPropertyName("activity_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ActivityName { get; private set; }

    /// <summary>
    /// The outcome of the authorization request: Success, Failure, Error, Unknown
    /// </summary>
    [JsonPropertyName("status")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Status { get; private set; }

    /// <summary>
    /// The integer code of the outcome status
    /// </summary>
    [JsonPropertyName("status_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? StatusId { get; private set; }

    /// <summary>
    /// A unique identifier (UID) for the event category.
    /// </summary>
    [JsonPropertyName("category_uid")]
    public int CategoryId { get; protected set; }

    /// <summary>
    /// The name of the event category.
    /// </summary>
    [JsonPropertyName("category_name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Category { get; protected set; }

    /// <summary>
    /// The name of the event class.
    /// </summary>
    [Required]
    [JsonPropertyName("class_name")]
    public string ClassName { get; protected set; } = null!;

    /// <summary>
    /// A unique identifier (UID) for the event class.
    /// </summary>
    [Required]
    [JsonPropertyName("class_uid")]
    public int ClassId { get; protected set; }

    /// <summary>
    /// The number of times that events in the same logical group occurred during the event Start Time to End Time period.
    /// </summary>
    [JsonPropertyName("count")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Count { get; set; }

    /// <summary>
    /// The normalized event occurrence time or the finding creation time in Unix epoch format.
    /// Defaults to UTC Now. Use the SetTime method to change the time.
    /// </summary>
    [Required]
    [JsonPropertyName("time")]
    public long Time { get; private set; }

    /// <summary>
    /// The number of minutes that the reported event time is ahead or behind UTC, in the range -1,080 to +1,080.
    /// Defaults to 0 (UTC).
    /// </summary>
    [Range(-1080, 1080)]
    [JsonPropertyName("timezone_offset")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? TimezoneOffset { get; private set; }

    /// <summary>
    /// The name of the event type.
    /// </summary>
    [Required]
    [JsonPropertyName("type_name")]
    public string Type => $"{ClassName}: {ActivityName}";

    /// <summary>
    /// A unique identifier (UID) for the event type.
    /// </summary>
    [Required]
    [JsonPropertyName("type_uid")]
    public int TypeUid => (ClassId * 100) + ActivityId;

    /// <summary>
    /// The time when the event ended in Unix epoch format.
    /// </summary>
    [JsonPropertyName("end_time")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? EndTime { get; set; }

    /// <summary>
    /// The event duration or aggregate time, the amount of time the event covers from start_time to end_time in milliseconds.
    /// </summary>
    [JsonPropertyName("duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? Duration { get; set; }

    /// <summary>
    /// A human-readable description of the event.
    /// </summary>
    [JsonPropertyName("message")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }

    /// <summary>
    /// The raw event/finding data as received from the source.
    /// </summary>
    [JsonPropertyName("raw_data")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RawData { get; set; }

    /// <summary>
    /// The size of the raw data which was transformed into an OCSF event, in bytes.
    /// </summary>
    [JsonPropertyName("raw_data_size")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? RawDataSize => RawData?.Length;

    /// <summary>
    /// The severity level of the event.
    /// </summary>
    [JsonPropertyName("severity")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Severity { get; private set; }

    /// <summary>
    /// A numeric representation of the event severity (0-99).
    /// </summary>
    [JsonPropertyName("severity_id")]
    public int SeverityId { get; private set; }

    /// <summary>
    /// The time when the event started in Unix epoch format.
    /// </summary>
    [JsonPropertyName("start_time")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? StartTime { get; set; }

    /// <summary>
    /// The status detail contains additional information about the event/finding outcome.
    /// </summary>
    [JsonPropertyName("status_detail")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? StatusDetail { get; set; }

    /// <summary>
    /// The event status code, as reported by the event source.
    /// For example, in a Windows Failed Authentication event, this would be the value of 'Failure Code', e.g., 0x18.
    /// </summary>
    [JsonPropertyName("status_code")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? StatusCode { get; private set; }

    /// <summary>
    /// The metadata associated with the event or a finding.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Metadata Metadata { get; } = new();

    /// <summary>
    /// The attributes that are not mapped to the event schema. The names and values of those attributes are specific to the event source.
    /// </summary>
    [JsonPropertyName("unmapped")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, object>? UnmappedFields { get; private set; }

    /// <summary>
    /// Add an unmapped field to the event.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public TEvent AddUnmappedField(string name, object value)
    {
        UnmappedFields ??= new Dictionary<string, object>();
        UnmappedFields.Add(name, value);
        return (TEvent)this;
    }

    /// <summary>
    /// Specify the status for this event.
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public TEvent WithStatus(Status status)
    {
        Status = status.Name;
        StatusId = status.Id;
        return (TEvent)this;
    }

    /// <summary>
    /// Sets the Time and the TimezoneOffset according to the values in DateTimeOffset.
    /// </summary>
    /// <param name="time"></param>
    public TEvent OccurredAt(DateTimeOffset time)
    {
        Time = time.ToUnixTimeMilliseconds();
        TimezoneOffset = (int)time.Offset.TotalMinutes;

        return (TEvent)this;
    }

    /// <summary>
    /// Sets the severity of the event.
    /// </summary>
    /// <param name="severity"></param>
    /// <returns></returns>
    public TEvent WithSeverity(Severity severity)
    {
        Severity = severity.Name;
        SeverityId = severity.Id;
        return (TEvent)this;
    }

    /// <inheritdoc/>
    public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(ActivityName))
        {
            yield return new ValidationResult("Activity name is required.", new[] { nameof(ActivityName) });
        }

        if (string.IsNullOrWhiteSpace(ClassName))
        {
            yield return new ValidationResult("Class name is required.", new[] { nameof(ClassName) });
        }

        if (Time == 0)
        {
            yield return new ValidationResult("Event time is required.", new[] { nameof(Time) });
        }

        if (string.IsNullOrWhiteSpace(Status))
        {
            yield return new ValidationResult("Status is required.", new[] { nameof(Status) });
        }

        if (string.IsNullOrWhiteSpace(Severity))
        {
            yield return new ValidationResult("Severity is required.", new[] { nameof(Severity) });
        }
    }

    /// <summary>
    /// Validates the event and returns a collection of validation results using the default validation context.
    /// </summary>
    public IEnumerable<ValidationResult> Validate()
    {
        return Validate(new ValidationContext(this));
    }
}
