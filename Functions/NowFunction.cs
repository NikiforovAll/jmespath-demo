using DevLab.JmesPath.Functions;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace jmespath_intro.Functions;

public class NowFunction : JmesPathFunction
{
    private const string DefaultDateFormat = "o";

    public NowFunction() : base("now", minCount: 0, variadic: true) { }

    public override JToken Execute(params JmesPathFunctionArgument[] args)
    {
        var format = args is { Length: > 0 } x
            ? x[0].Token
            : DefaultDateFormat;

        return new JValue(DateTimeOffset.UtcNow.ToString(format.ToString()));
    }
}
