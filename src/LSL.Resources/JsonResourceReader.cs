using System;
using LSL.Resources.Infrastructure;

namespace LSL.Resources;

internal class JsonResourceReader(ReadJsonResourceSettings readJsonResourceSettings) : IJsonResourceReader
{
    public object ReadJsonResource(Type type) =>
        ResourceHelper.InternalReadJsonResource(
            type.AssertNotNull(nameof(type)),
            readJsonResourceSettings);
}