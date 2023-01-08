using DevLab.JmesPath.Functions;
using DevLab.JmesPath.Utils;
using Newtonsoft.Json.Linq;

namespace jmespath_intro.Functions;

public class NowFunction : JmesPathFunction
{
    public NowFunction() : base("now", 0, true)
    {
    }

    public override JToken Execute(params JmesPathFunctionArgument[] args)
    {
        var format = args is { Length: > 0 } x ? Evaluate(x[0]) : "o";

        return new JValue(DateTimeOffset.UtcNow.ToString(format.ToString()));
    }

    private static JToken Evaluate(JmesPathFunctionArgument argument)
    {
        return argument.Token;
    }
}
