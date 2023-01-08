using DevLab.JmesPath.Functions;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace jmespath_intro.Functions;

public class DateFormatFunction : JmesPathFunction
{
    public DateFormatFunction() : base("date_format", 2)
    {
    }

    public override JToken Execute(params JmesPathFunctionArgument[] args)
    {
        var date = DateTime.ParseExact(
            Evaluate(args[0]).ToString(),
            formats: new[] { "o", "u", "s" },
            CultureInfo.InvariantCulture);
        var format = Evaluate(args[1]);

        return new JValue(date.ToString(format.ToString()));
    }

    private static JToken Evaluate(JmesPathFunctionArgument argument)
    {
        return argument.Token;
    }
}
