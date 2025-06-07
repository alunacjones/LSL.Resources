using System;

namespace LSL.Resources.Infrastructure;

/// <summary>
/// Guard extensions
/// </summary>
internal static class GuardExtensions
{
    private const string _baseError = "Argument cannot be null";

    public static T AssertNotNull<T>(
        this T value,
        string parameterName,
        string extraInformation = null) => value ?? throw new ArgumentNullException(
            parameterName,
            extraInformation == null
                ? _baseError
                : $"{_baseError}. {extraInformation}");
}