using Corvus.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using TortisDev.Ocsf;
using Xunit.Abstractions;

namespace Ocsf.Tests;

public class BaseEventTests
{
    ITestOutputHelper _output;

    public BaseEventTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void ValidatesAgainstJsonSchema()
    {
        var schema = Corvus.Json.Validator.JsonSchema.FromFile("schemas/base_event.json");
        var baseEvent = new BaseEvent(BaseEvent.Activity.Unknown);
        var jsonElement = JsonSerializer.SerializeToElement(baseEvent);
        var jsonString = JsonSerializer.Serialize(baseEvent,
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
    public void BaseEventCategoryShouldBe0()
    {
        var sot = new BaseEvent(BaseEvent.Activity.Unknown);
        Assert.Equal(0, sot.CategoryId);
        Assert.Equal("Uncategorized", sot.Category);
    }

    [Fact]
    public void BaseEventClassShouldBe0()
    {
        var sot = new BaseEvent(BaseEvent.Activity.Other("Testing class"));
        Assert.Equal(0, sot.ClassId);
        Assert.Equal("Base Event", sot.ClassName);
    }
}