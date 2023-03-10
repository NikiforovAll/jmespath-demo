using DevLab.JmesPath.Functions;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace jmespath_intro.Functions;

public class TimeDifferenceFunction : JmesPathFunction
{
    public TimeDifferenceFunction() : base("time_difference", 3)
    {
    }

    public override JToken Execute(params JmesPathFunctionArgument[] args)
    {
        var minuend = DateTime.ParseExact(
            args[1].Token.ToString(),
            formats: new[] { "o", "u", "s" },
            CultureInfo.InvariantCulture);

        var subtrahend = DateTime.ParseExact(
            args[2].Token.ToString(),
            formats: new[] { "o", "u", "s" },
            CultureInfo.InvariantCulture);

        var difference = minuend - subtrahend;

        var units = args[0].Token.ToString();

        var total = units switch
        {
            "days" => difference.TotalDays,
            "hours" => difference.TotalHours,
            "minutes" => difference.TotalMinutes,
            _ => throw new ArgumentOutOfRangeException()
        };

        return new JValue(total);
    }
}
