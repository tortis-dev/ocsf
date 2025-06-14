using Corvus.Json;
using System.Text.Json;
using TortisDev.Ocsf;
using TortisDev.Ocsf.Iam;
using Xunit.Abstractions;

namespace Ocsf.Tests;

public class GroupManagementEventTests
{
    ITestOutputHelper _output;

    public GroupManagementEventTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ValidatesAgainstJsonSchema()
    {
        var schema = Corvus.Json.Validator.JsonSchema.FromFile("schemas/group_management.json");
        var evt = new GroupManagementEvent(new Group { Name = "Engineering" }, GroupManagementEvent.Activity.Unknown)
            .WithSeverity(Severity.Informational)
            .By(new Actor
            {
                ApplicationName = "Tortis IAM",
                User = new User{Name = "tstark"}.OfType(User.UserType.Admin)
            })
        ;
        evt.User = new User {Name = "efudd"}.OfType(User.UserType.User);

        var jsonElement = JsonSerializer.SerializeToElement(evt);
        var jsonString = JsonSerializer.Serialize(evt,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });
        _output.WriteLine(jsonString);

        var results = schema.Validate(jsonElement, ValidationLevel.Detailed);

        if (!results.IsValid)
        {
            foreach (var result in results.Results)
                _output.WriteLine(result.ToString());
        }

        Assert.True(results.IsValid);
    }

    [Fact]
    public void HasCategory()
    {
        var sot = new GroupManagementEvent(new Group { Name = "Engineering" }, GroupManagementEvent.Activity.Unknown);
        Assert.Equal(3, sot.CategoryId);
        Assert.Equal("Identity & Access Management", sot.Category);
    }

    [Fact]
    public void HasClass()
    {
        var sot = new GroupManagementEvent(new Group { Name = "Engineering" }, GroupManagementEvent.Activity.Other("Testing"));
        Assert.Equal(3006, sot.ClassId);
        Assert.Equal("Group Management", sot.ClassName);
    }
}
