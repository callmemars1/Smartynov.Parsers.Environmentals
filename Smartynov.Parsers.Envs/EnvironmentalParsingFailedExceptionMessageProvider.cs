using System.Reflection;
using System.Text;
using Smartynov.Parsers.Core;

namespace Smartynov.Parsers.Envs;

public class EnvironmentalParsingFailedExceptionMessageProvider : IExceptionMessageProvider
{
    private readonly Type? _argumentsType;

    public EnvironmentalParsingFailedExceptionMessageProvider()
    {
    }

    public EnvironmentalParsingFailedExceptionMessageProvider(Type argumentsType)
    {
        _argumentsType = argumentsType;
    }

    public string GenerateMessage(ICollection<PropertyParsingException> propertiesExceptions)
    {
        var messageBuilder = new StringBuilder();

        if (_argumentsType != null)
        {
            var descriptionAttribute = _argumentsType.GetCustomAttributes<DescriptionAttribute>().FirstOrDefault();
            if (descriptionAttribute != null)
                messageBuilder
                    .Append("\n\n")
                    .Append(descriptionAttribute.Description)
                    .AppendLine()
                    .Append($"({_argumentsType.Name})")
                    .Append("\n\n");
            else
                messageBuilder
                    .Append("\n\n")
                    .Append(_argumentsType.Name)
                    .Append("\n\n");
        }

        foreach (var exception in propertiesExceptions)
        {
            messageBuilder.Append(
                $"{exception.Option.Name} (Property: {exception.Property.Name}){(exception.Option.HelpText != string.Empty ? string.Concat(" --- ", exception.Option.HelpText) : "")}\n" +
                $"Error: {exception.Error}").Append("\n\n");
        }

        return messageBuilder.ToString();
    }
}