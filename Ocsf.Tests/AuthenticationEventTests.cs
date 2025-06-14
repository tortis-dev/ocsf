using Corvus.Json;
using System.Text.Json;
using TortisDev.Ocsf;
using TortisDev.Ocsf.Iam;
using Xunit.Abstractions;

namespace Ocsf.Tests;

public class AuthenticationEventTests
{
    ITestOutputHelper _output;

    public AuthenticationEventTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ValidValidatesAgainstJsonSchemaate()
    {
        var schema = Corvus.Json.Validator.JsonSchema.FromFile("schemas/authentication.json");
        var evt = new AuthenticationEvent(new  User { Name = "efudd" }.OfType(User.UserType.User), AuthenticationEvent.Activity.Logon)
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
        var sot = new AuthenticationEvent(new User { Name = "efudd" }.OfType(User.UserType.User), AuthenticationEvent.Activity.Logon);
        Assert.Equal(3, sot.CategoryId);
        Assert.Equal("Identity & Access Management", sot.Category);
    }

    [Fact]
    public void HasClass()
    {
        var sot = new AuthenticationEvent(new User { Name = "efudd" }.OfType(User.UserType.User), AuthenticationEvent.Activity.Logon);
        Assert.Equal(3002, sot.ClassId);
        Assert.Equal("Authentication", sot.ClassName);
    }
}
