using Corvus.Json;
using System.Text.Json;
using TortisDev.Ocsf;
using TortisDev.Ocsf.Iam;
using Xunit.Abstractions;

namespace Ocsf.Tests;


public class AccountChangeTests
{
    ITestOutputHelper _output;

    public AccountChangeTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact(DisplayName = "AccountChangeEvent Validates Against Json Schema")]
    public void ValiValidatesAgainstJsonSchemadate()
    {
        var schema = Corvus.Json.Validator.JsonSchema.FromFile("schemas/account_change.json");
        var evt = new AccountChangeEvent(new User { Name = "efudd" }.OfType(User.UserType.User), AccountChangeEvent.Activity.PasswordChange)
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

    [Fact(DisplayName = "A AccountChangeEvent should be valid.")]
    public void ValidationSuccess()
    {
        var evt = new AccountChangeEvent(new User { Name = "efudd" }.OfType(User.UserType.User),  AccountChangeEvent.Activity.PasswordChange)
            .WithSeverity(Severity.Informational)
            .By(new Actor
            {
                ApplicationName = "Tortis IAM",
                User = new User { Name = "tstark" }.OfType(User.UserType.Admin)
            });

        var results = evt.Validate();

        if (results.Any())
        {
            foreach (var result in results)
                _output.WriteLine(result.ErrorMessage);
        }

        Assert.False(results.Any());
    }

    [Fact(DisplayName = "A AccountChangeEvent with an invalid user should be invalid.")]
    public void ValidateBadUser()
    {
        var evt = new AccountChangeEvent(new User (),  AccountChangeEvent.Activity.PasswordChange)
            .WithSeverity(Severity.Informational)
            .By(new Actor
            {
                ApplicationName = "Tortis IAM",
                User = new User { Name = "tstark" }.OfType(User.UserType.Admin)
            });

        var results = evt.Validate();

        if (results.Any())
        {
            foreach (var result in results)
                _output.WriteLine(result.ErrorMessage);
        }

        Assert.True(results.Any());
    }

    [Fact(DisplayName = "AccountChangeEvent Category should be 3, Identity & Access Management")]

    public void HasCategory()
    {
        var sot = new AccountChangeEvent(new User { Name = "efudd" }.OfType(User.UserType.User),  AccountChangeEvent.Activity.PasswordChange);
        Assert.Equal(3, sot.CategoryId);
        Assert.Equal("Identity & Access Management", sot.Category);
    }

    [Fact(DisplayName = "AccountChangeEvent Class should be 3001, Account Change")]
    public void HasClass()
    {
        var sot = new AccountChangeEvent(new User { Name = "efudd" }.OfType(User.UserType.User),  AccountChangeEvent.Activity.PasswordChange);
        Assert.Equal(3001, sot.ClassId);
        Assert.Equal("Account Change", sot.ClassName);
    }

}