using System;

namespace LSL.Resources;

public interface IJsonResourceReader
{
    object ReadJsonResource(Type type);
}
