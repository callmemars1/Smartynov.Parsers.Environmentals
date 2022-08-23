using System.Reflection;
using Smartynov.Parsers.Core;

namespace Smartynov.Parsers.Envs;

public class EnvironmentalParser<TObject>
    : IParser<TObject> where TObject : new()
{
    public EnvironmentalParser()
    {
    }

    public ParsingResult<TObject> Parse()
    {
        var obj = new TObject();
        var objType = typeof(TObject);
        ICollection<PropertyParsingException> errors = new LinkedList<PropertyParsingException>();

        var properties = objType.GetProperties()
            .Where(p => p.GetCustomAttributes<EnvironmentalOptionAttribute>().Any());

        foreach (var property in properties)
        {
            var attribute = property.GetCustomAttribute<EnvironmentalOptionAttribute>()!;
            try
            {
                var value = ParseForProperty(property, attribute);
                property.SetValue(obj, value);
            }
            catch (PropertyParsingException e)
            {
                if (attribute.Required)
                    errors.Add(e);
            }
        }

        return errors.Any()
            ? new ParsingResult<TObject>(new EnvironmentalParsingException(errors))
            : new ParsingResult<TObject>(obj);
    }

    private static object ParseForProperty(PropertyInfo property, EnvironmentalOptionAttribute attribute)
    {
        var value = Environment.GetEnvironmentVariable(attribute.Name);
        if (value == null)
            throw new PropertyParsingException(property, attribute,
                $"Required environmental variable is not defined");

        try
        {
            return Convert.ChangeType(value, property.PropertyType);
        }
        catch (Exception e)
        {
            throw new PropertyParsingException(property, attribute,
                $"Could not convert value \"{value}\" ({value.GetType().Name}) of environmental variable to {property.PropertyType}");
        }
    }
}