# Tortis OCSF

![Build Status](https://github.com/jason/tortis/actions/workflows/ci.yml/badge.svg)


A C#/.Net implementation of the Open Cybersecurity Schema Framework (OCSF) for creating security events in .Net applications for export to a SIEM.

### User Access Management Events Example

```csharp
// Create a user access management event for assigning privileges
var user = new User 
{
    Uid = "john.doe",
    Name = "John Doe",
    Domain = "example.com"
};

var resources = new[] 
{
    new Resource 
    { 
        Name = "financial-reports",
        Type = "folder"
    }
};

var userAccessEvent = new UserAccessManagementEvent(user, UserAccessManagementEvent.Activity.AssignPrivileges)
{
    Resources = resources,
    Privileges = new[] { "read", "write" }
};

// Publishing the event to Kafka
using var producer = new ProducerBuilder<string, string>(producerConfig).Build();
var jsonString = JsonSerializer.Serialize(userAccessEvent, new JsonSerializerOptions 
{ 
    WriteIndented = true 
});

await producer.ProduceAsync("user-access-events", new Message<string, string> 
{ 
    Key = user.UserId,
    Value = jsonString 
});
```

This example demonstrates creating an event that logs when a user is granted read and write privileges to a specific resource and publishing the event to a Kafka topic using the Confluent.Kafka client.

### Validation

It is recommended to validate the event before publishing.

```csharp
IEnumerable<ValidationResult> results = userAccessEvent.Validate();

bool isValid = !results.Any();
```