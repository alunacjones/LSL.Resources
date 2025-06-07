using System;
using LSL.Resources.Infrastructure;

namespace LSL.Resources;

internal class JsonResourceReader(ReadJsonResourceSettings readJsonResourceSettings) : IJsonResourceReader
{
    public object ReadJsonResource(Type type, Action<JsonResourceReaderSettings> configurator = null)
    {
        var settings = configurator == null
            ? readJsonResourceSettings
            : readJsonResourceSettings.CloneWith(RunConfigurator(configurator, new JsonResourceReaderSettings()));

        return ResourceHelper.InternalReadJsonResource(
            type.AssertNotNull(nameof(type)),
            settings);

        static BaseReadJsonResourceSettings<JsonResourceReaderSettings> RunConfigurator(
            Action<JsonResourceReaderSettings> configurator,
            JsonResourceReaderSettings settings)
        {
            configurator(settings);
            return settings;
        }
    }
}
