using System;
using System.Text.Json;
using LSL.Resources.Infrastructure;

namespace LSL.Resources;

/// <summary>
/// ReadJsonResourceSettings
/// </summary>
public sealed class ReadJsonResourceSettings : BaseSettings<ReadJsonResourceSettings>
{
    internal ReadJsonResourceSettings() => Self = this;

    internal string ResourceNamePrefix { get; private set; }
    internal string ResourceNameEndsWith { get; private set; }
    internal Action<JsonSerializerOptions> OptionsConfigurator { get; private set; } = _ => { };

    /// <summary>
    /// Allows provide a delegate to configure
    /// the <see cref="JsonSerializerOptions"/> 
    /// that will be used during deserialization
    /// </summary>
    /// <param name="optionsConfigurator"></param>
    /// <returns></returns>
    public ReadJsonResourceSettings ConfigureJsonDeserializerOptions(Action<JsonSerializerOptions> optionsConfigurator)
    {
        OptionsConfigurator = optionsConfigurator.AssertNotNull(nameof(optionsConfigurator));
        return this;
    }

    /// <summary>
    /// Allows for overriding the default
    /// behaviour of using the type name to
    /// match the resource name
    /// </summary>
    /// <param name="resourceNameEndsWith"></param>
    /// <returns></returns>
    public ReadJsonResourceSettings MatchingResourceEndsWith(string resourceNameEndsWith)
    {
        ResourceNameEndsWith = resourceNameEndsWith.AssertNotNull(nameof(resourceNameEndsWith));
        return this;
    }

    /// <summary>
    /// Allows for prefixing of the partial resource
    /// name so allow for sub folders but still
    /// allow the full type name to be utilised in the name
    /// </summary>
    /// <param name="resourceNamePrefix"></param>
    /// <returns></returns>
    public ReadJsonResourceSettings WithResourceNamePrefixOf(string resourceNamePrefix)
    {
        ResourceNamePrefix = resourceNamePrefix.AssertNotNull(nameof(resourceNamePrefix));
        return this;
    }
}