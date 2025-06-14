# Tortis OCSF

A C#/.Net implementation of the Open Cybersecurity Schema Framework (OCSF) for creating security events in .Net applications for export to a SIEM.

### User Access Management Events Example

```csharp
// Create a user access management event for assigning privileges
var user = new User 
{
    UserId = "john.doe",
    UserName = "John Doe",
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

var event = new UserAccessManagementEvent(user, UserAccessManagementEvent.Activity.AssignPrivileges)
{
    Resources = resources,
    Privileges = new[] { "read", "write" }
};
```

This example demonstrates creating an event that logs when a user is granted read and write privileges to a specific resource.