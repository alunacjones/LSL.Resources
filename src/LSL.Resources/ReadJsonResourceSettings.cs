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
}