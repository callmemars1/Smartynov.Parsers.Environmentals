namespace Smartynov.Parsers.Core;

public interface IParser<TObject> where TObject : new()
{
    ParsingResult<TObject> Parse();
}