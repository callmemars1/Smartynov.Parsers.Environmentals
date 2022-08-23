namespace Smartynov.Parsers.Core;

[AttributeUsage(AttributeTargets.Property)]
public abstract class OptionAttribute : Attribute
{
    public string Name { get; }

    public bool Required { get; init; } = true;

    public string HelpText { get; init; } = "";

    public OptionAttribute(string name)
    {
        Name = name;
    }
}