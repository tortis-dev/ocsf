namespace TortisDev.Ocsf;

/// <summary>
/// Represents the base event class for the OCSF schema. The base event class defines
/// common fields found in log/event data that are used by other classes.
/// The base event is a generic and concrete event. It also defines a set of attributes available in most event classes.
/// As a generic event that does not belong to any event category,
/// it could be used to log events that are not otherwise defined by the schema.
/// </summary>
[PublicAPI]
public class BaseEvent : AbstractEvent<BaseEvent, BaseEvent.Activity>
{
    /// <summary>
    /// Defines activities for the Base Event.
    /// </summary>
    public class Activity : IActivity
    {
        /// <summary>
        /// The event activity is unknown.
        /// </summary>
        public static readonly Activity Unknown = new(0, "Unknown");

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
    /// Creates a new Base Event.
    /// </summary>
    public BaseEvent(Activity activity) : base(activity)
    {
        CategoryId = 0;
        Category = "Uncategorized";

        ClassId = 0;
        ClassName = "Base Event";
    }
}