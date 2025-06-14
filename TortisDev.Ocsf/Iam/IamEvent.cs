using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TortisDev.Ocsf.Iam;

/// <summary>
/// Base class for Identity and Access Management (IAM) events
/// </summary>
[PublicAPI]
public abstract class IamEvent<TEvent, TActivity> : AbstractEvent<TEvent, TActivity>
    where TEvent : IamEvent<TEvent, TActivity>
    where TActivity : IActivity
{
    /// <summary>
    /// Initializes a new instance of the IamEvent class.
    /// </summary>
    protected IamEvent(TActivity activity) : base(activity)
    {
        CategoryId = 3; // Identity & Access Management
        Category = "Identity & Access Management";
    }

    /// <summary>
    /// The actor object describes details about the user/role/process that was the source of the activity.
    /// Note that this is not the threat actor of a campaign but may be part of a campaign.
    /// </summary>
    [JsonPropertyName("actor")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Actor? Actor { get; set; }

    /// <summary>
    /// Who is making the change?
    /// </summary>
    /// <param name="actor"></param>
    /// <returns></returns>
    public TEvent By(Actor actor)
    {
        Actor = actor;
        return (TEvent)this;
    }

    /// <inheritdoc/>
    public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var baseResults = base.Validate(validationContext);

        foreach (var result in baseResults)
        {
            yield return result;
        }

        if (Actor is not null)
        {
            foreach (var result in Actor.Validate(validationContext))
            {
                yield return result;
            }
        }
    }
}