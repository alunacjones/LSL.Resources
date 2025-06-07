using System;
using System.Reflection;
using System.Text.Json;
using LSL.Resources.Infrastructure;

namespace LSL.Resources;

/// <summary>
/// ReadJsonResourceSettings
/// </summary>
public sealed class ReadJsonResourceSettings : BaseReadJsonResourceSettings<ReadJsonResourceSettings>
{
    internal ReadJsonResourceSettings() => Self = this;

    internal Action<JsonSerializerOptions> OptionsConfigurator { get; private set; } = _ => { };

    internal Assembly Assembly { get; private set; }

    /// <summary>
    /// Set the assembly to search for a resource
    /// </summary>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public ReadJsonResourceSettings FromAssembly(Assembly assembly)
    {
        Assembly = assembly.AssertNotNull(nameof(assembly));
        return this;
    }

    /// <summary>
    /// Sets the assembly to search for a resource
    /// to the assembly that contains <paramref name="type"/>
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public ReadJsonResourceSettings FromAssemblyOfType(Type type) => FromAssembly(type.AssertNotNull(nameof(type)).Assembly);

    /// <summary>
    /// Sets the assembly to search for a resource
    /// to the assembly that contains <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public ReadJsonResourceSettings FromAssemblyOfType<T>() => FromAssemblyOfType(typeof(T));

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

    internal ReadJsonResourceSettings CloneWith(IBaseReadJsonResourceSettings baseReadJsonResourceSettings)
    {
        var result = new ReadJsonResourceSettings()
        {
            Assembly = Assembly,
            OptionsConfigurator = OptionsConfigurator,
        };

        if (baseReadJsonResourceSettings.ResourceNameEndsWith is not null)
        {
            result.MatchingResourceEndsWith(baseReadJsonResourceSettings.ResourceNameEndsWith);
        }

        if (baseReadJsonResourceSettings.ResourceNamePrefix is not null)
        {
            result.WithResourceNamePrefixOf(baseReadJsonResourceSettings.ResourceNamePrefix);
        }

        return result;
    }
}