using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using FluentAssertions;
using LSL.Resources.DotNetFiddle;

namespace LSL.Resources.Tests;

public class ResourceHelperTests
{
    [Test]
    public void ReadJsonResource_GivenAValidResource_ItShouldReturnTheExpectedResult()
    {
        ResourceHelper
            .ReadJsonResource<JsonTestClass>(c => c
                .FromAssemblyOfType<JsonTestClass>())
            .Should()
            .BeEquivalentTo(new JsonTestClass
            {
                Name = "Als",
                Age = 12
            });
    }

    [Test]
    public void ReadJsonResource_WithANullResourceNameOverride_ShouldThrowANullArgumentException()
    {
        new Action(() => ResourceHelper
            .ReadJsonResource<JsonTestClass>(c => c
                .FromAssemblyOfType<JsonTestClass>()
                .MatchingResourceEndsWith(null)))
            .Should()
            .ThrowExactly<ArgumentNullException>()
            .WithMessage("Argument cannot be null (Parameter 'resourceNameEndsWith')");
    }

    [Test]
    public void ReadJsonResource_WithANullPrefix_ShouldThrowANullArgumentException()
    {
        new Action(() => ResourceHelper
            .ReadJsonResource<JsonTestClass>(c => c
                .FromAssemblyOfType<JsonTestClass>()
                .WithResourceNamePrefixOf(null)))
            .Should()
            .ThrowExactly<ArgumentNullException>()
            .WithMessage("Argument cannot be null (Parameter 'resourceNamePrefix')");
    }

    [Test]
    public void ReadJsonResource_WithANullAssembly_ShouldThrowANullArgumentException()
    {
        new Action(() => ResourceHelper
            .ReadJsonResource<JsonTestClass>(c => c
                .FromAssembly(null)))
            .Should()
            .ThrowExactly<ArgumentNullException>()
            .WithMessage("Argument cannot be null (Parameter 'assembly')");
    }

    [Test]
    public void ReadJsonResource_WithANullType_ShouldThrowANullArgumentException()
    {
        new Action(() => ResourceHelper
            .ReadJsonResource<JsonTestClass>(c => c
                .FromAssemblyOfType(null)))
            .Should()
            .ThrowExactly<ArgumentNullException>()
            .WithMessage("Argument cannot be null (Parameter 'type')");
    }

    [Test]
    public void ReadJsonResourceWithNameOverride_GivenAValidResource_ItShouldReturnTheExpectedResult()
    {
        ResourceHelper
            .ReadJsonResource<JsonTestClass>(c => c
                .FromAssemblyOfType<JsonTestClass>()
                .MatchingResourceEndsWith("other.json")
            )
            .Should()
            .BeEquivalentTo(new JsonTestClass
            {
                Name = "Als2",
                Age = 13
            });
    }

    [Test]
    public void ReadJsonResourcePrefixOverride_GivenAValidResource_ItShouldReturnTheExpectedResult()
    {
        ResourceHelper
            .ReadJsonResource<JsonTestClass>(c => c
                .FromAssemblyOfType<ResourceHelperTests>()
                .WithResourceNamePrefixOf("SubFolder")
            )
            .Should()
            .BeEquivalentTo(new JsonTestClass
            {
                Name = "Als3",
                Age = -21
            });
    }

    [Test]
    public void JsonReaderPrefixOverride_GivenAValidResource_ItShouldReturnTheExpectedResult()
    {
        ResourceHelper
            .BuildJsonReader(c => c
                .FromAssemblyOfType<ResourceHelperTests>()
                .WithResourceNamePrefixOf("CaseInsensitive")
                .ConfigureJsonDeserializerOptions(c => c.PropertyNameCaseInsensitive = true)
            )
            .ReadJsonResource<JsonTestClass>()
            .Should()
            .BeEquivalentTo(new JsonTestClass
            {
                Name = "AlsLower",
                Age = -25
            });
    }

    [Test]
    public void JsonReaderPrefixOverrideAtTheReadCall_GivenAValidResource_ItShouldReturnTheExpectedResult()
    {
        ResourceHelper
            .BuildJsonReader(c => c
                .FromAssemblyOfType<ResourceHelperTests>()
                .ConfigureJsonDeserializerOptions(c => c.PropertyNameCaseInsensitive = true)
            )
            .ReadJsonResource<JsonTestClass>(c => c
                .WithResourceNamePrefixOf("CaseInsensitive"))
            .Should()
            .BeEquivalentTo(new JsonTestClass
            {
                Name = "AlsLower",
                Age = -25
            });
    }

    [Test]
    public void JsonReaderResourceNameOverrideAtTheReadCall_GivenAValidResource_ItShouldReturnTheExpectedResult()
    {
        ResourceHelper
            .BuildJsonReader(c => c
                .FromAssemblyOfType<JsonTestClass>()
                .ConfigureJsonDeserializerOptions(c => c.PropertyNameCaseInsensitive = true)
            )
            .ReadJsonResource<JsonTestClass>(c => c
                .MatchingResourceEndsWith("other.json"))
            .Should()
            .BeEquivalentTo(new JsonTestClass
            {
                Name = "Als2",
                Age = 13
            });
    }

    [Test]
    public void JsonReaderResourceNameOverrideAtTheReadCallAndADictionaryType_GivenAValidResource_ItShouldReturnTheExpectedResult()
    {
        ResourceHelper
            .BuildJsonReader(c => c
                .FromAssemblyOfType<ResourceHelperTests>()
                .ConfigureJsonDeserializerOptions(c => c.PropertyNameCaseInsensitive = true)
            )
            .ReadJsonResource<IDictionary<string, string>>(c => c
                .MatchingResourceEndsWith("Dictionary.json"))
            .Should()
            .BeEquivalentTo(new Dictionary<string, string>
            {
                { "Name", "Als2" },
                { "Age",  "13" }
            });
    }

    [Test]
    public void JsonReaderResourceNameOverrideAtTheReadCallWithNoAssemblySetup_GivenAValidResource_ItShouldThrowTheExpectedException()
    {
        new Action(() => ResourceHelper
            .BuildJsonReader(c => c
                .ConfigureJsonDeserializerOptions(c => c.PropertyNameCaseInsensitive = true)
            )
            .ReadJsonResource<JsonTestClass>(c => c
                .MatchingResourceEndsWith("other.json")))
            .Should()
            .ThrowExactly<ArgumentNullException>()
            .WithMessage("Argument cannot be null. It looks like you haven't setup an Assembly via the FromAssembly or FromAssemblyOfType methods of your configurator (Parameter 'Assembly')");
    }    

    [Test]
    public void ReadJsonResourceWithEnum_GivenAValidResource_ItShouldReturnTheExpectedResult()
    {
        ResourceHelper
            .ReadJsonResource<JsonTestClassWithEnum>(c => c
                .FromAssemblyOfType<JsonTestClass>()
                .ConfigureJsonDeserializerOptions(c => c.Converters.Add(new JsonStringEnumConverter())))
            .Should()
            .BeEquivalentTo(new JsonTestClassWithEnum
            {
                Name = "OtherAls",
                Age = 10,
                Answer = YesOrNo.No
            });
    }

    [Test]
    public void GetResourceStream_GivenAnInvalidResourceName_ItShouldThrowFileNotFoundException()
    {
        new Action(() => GetType().Assembly.GetResourceStream("not-real"))
            .Should()
            .ThrowExactly<FileNotFoundException>()
            .WithMessage("Could not locate a resource whose name ends with 'not-real' from assembly 'LSL.Resources.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'.")
            .And
            .FileName
            .Should()
            .Be("not-real");

    }

    [Test]
    public void ReadStringResource_GivenAValidResource_ItShouldReturnTheExpectedResult()
    {
        GetType()
            .Assembly
            .ReadStringResource("text-file.txt")
            .Should()
            .Be("Text file");
    }

    [Test]
    public void GetResourceStream_GivenANullAssembly_ItShouldThrowANullArgumentException()
    {
        new Action(() => GetType().Assembly.GetResourceStream(null))
            .Should()
            .ThrowExactly<ArgumentNullException>()
            .WithMessage("Argument cannot be null (Parameter 'resourceNameEndsWith')");
    }

    [Test]
    public void ReadStringResource_GivenANullAssembly_ItShouldThrowANullArgumentException()
    {
        new Action(() => ((Assembly)null).GetResourceStream(null))
            .Should()
            .ThrowExactly<ArgumentNullException>()
            .WithMessage("Argument cannot be null (Parameter 'assembly')");
    }

    [Test]
    public void ReadJsonResourceWithNoConfiguration_GivenAValidResource_ItShouldReturnTheExpectedResult()
    {
        ResourceHelper
            .ReadJsonResource<MyTestClass>()
            .Should()
            .BeEquivalentTo(new MyTestClass
            {
                Name = "Tally Ho"
            });
    }
    internal class MyTestClass
    {
        public string Name { get; set; }
    }
}