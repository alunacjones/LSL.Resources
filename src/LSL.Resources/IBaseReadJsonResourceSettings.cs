namespace LSL.Resources;

internal interface IBaseReadJsonResourceSettings
{
    string ResourceNameEndsWith { get; }
    string ResourceNamePrefix { get; }
}