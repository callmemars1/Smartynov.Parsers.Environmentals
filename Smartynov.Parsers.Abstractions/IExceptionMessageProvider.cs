namespace Smartynov.Parsers.Core;

public interface IExceptionMessageProvider
{
    string GenerateMessage(ICollection<PropertyParsingException> propertiesExceptions);
}