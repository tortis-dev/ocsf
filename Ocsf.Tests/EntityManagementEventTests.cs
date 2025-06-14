using Corvus.Json;
using System.Text.Json;
using TortisDev.Ocsf;
using TortisDev.Ocsf.Iam;
using Xunit.Abstractions;

namespace Ocsf.Tests;

public class EntityManagementEventTests
{
    ITestOutputHelper _output;

    public EntityManagementEventTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(Skip = "This test is skipped because the JSON Schema fails to load.")]
    public void ValidatesAgainstJsonSchema()
    {
        var schema = Corvus.Json.Validator.JsonSchema.FromFile("schemas/entity_management.json");

        var evt = new EntityManagementEvent(ManagedEntity.OfTypeUser(new User { Name = "efudd" }), EntityManagementEvent.Activity.Create)
            .WithSeverity(Severity.Informational)
            .By(new Actor
            {
                ApplicationName = "Tortis IAM",
                User = new User { Name = "tstark" }.OfType(User.UserType.Admin)
            });

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

    [Fact(DisplayName = "EntityManagementEvent Validates when correctly configured.")]
    public void ValidationSuccess()
    {
        var evt = new EntityManagementEvent(ManagedEntity.OfTypeUser(new User { Name = "efudd" }), EntityManagementEvent.Activity.Create)
            .WithSeverity(Severity.Informational)
            .By(new Actor
            {
                ApplicationName = "Tortis IAM",
                User = new User { Name = "tstark" }.OfType(User.UserType.Admin)
            });

        var results = evt.Validate();
        var isValid = !results.Any();
        if (!isValid)
        {
            foreach (var result in results)
                _output.WriteLine(result.ErrorMessage);
        }

        Assert.True(isValid);
    }

    [Fact]
    public void HasCategory()
    {
        var sot = new EntityManagementEvent(ManagedEntity.OfUnknownType(), EntityManagementEvent.Activity.Create);
        Assert.Equal(3, sot.CategoryId);
        Assert.Equal("Identity & Access Management", sot.Category);
    }

    [Fact]
    public void HasClass()
    {
        var sot = new EntityManagementEvent(ManagedEntity.OfUnknownType(), EntityManagementEvent.Activity.Other("Testing class"));
        Assert.Equal(3004, sot.ClassId);
        Assert.Equal("Entity Management", sot.ClassName);
    }
}
