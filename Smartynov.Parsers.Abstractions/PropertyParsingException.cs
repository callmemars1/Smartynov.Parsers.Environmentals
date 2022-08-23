using System.Reflection;
using System.Runtime.InteropServices;

namespace Smartynov.Parsers.Core;

public class PropertyParsingException : Exception
{
    public PropertyInfo Property { get; }
    
    public OptionAttribute Option { get; }
    public string Error { get; }

    public PropertyParsingException(PropertyInfo property, OptionAttribute option, string error)
    {
        Property = property;
        Option = option;
        Error = error;
    }
}