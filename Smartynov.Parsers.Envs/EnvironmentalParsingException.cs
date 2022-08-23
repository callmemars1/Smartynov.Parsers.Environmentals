using Smartynov.Parsers.Core;

namespace Smartynov.Parsers.Envs;

public class EnvironmentalParsingException : ParsingException
{
    public EnvironmentalParsingException(ICollection<PropertyParsingException> propertyExceptions) : base(
        propertyExceptions, new EnvironmentalParsingFailedExceptionMessageProvider(propertyExceptions.FirstOrDefault()?.Property?.DeclaringType!))
    {
    }
}