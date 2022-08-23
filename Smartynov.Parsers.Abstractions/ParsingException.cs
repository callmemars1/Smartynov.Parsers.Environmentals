namespace Smartynov.Parsers.Core;

public abstract class ParsingException : Exception
{
    protected ParsingException(
        ICollection<PropertyParsingException> propertyExceptions,
        IExceptionMessageProvider provider
    ) : base(provider.GenerateMessage(propertyExceptions))
    {
    }
}