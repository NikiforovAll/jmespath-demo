using DevLab.JmesPath;
using jmespath_intro.Functions;
using Spectre.Console;
using Spectre.Console.Json;

var source = new StreamReader("./example.json")
    .ReadToEnd();

var parser = new JmesPath();
parser.FunctionRepository
    .Register<NowFunction>()
    .Register<DateFormatFunction>()
    .Register<TimeDifferenceFunction>();

var expressions = new (string, string)[]
{
    ("scalar", "balance"),
    ("projection", "{email: email, name: name}"),
    ("functions", "to_string(latitude)"),
    ("arrays", "friends[*].name"),
    ("filtering", "friends[?age > `20`].name"),
    ("aggregation", "{sum: sum(friends[*].age), names: join(',', friends[*].name)}"),
    ("now. ISO 8601", "now()"),
    ("now. Universal sortable date/time pattern", "now('u')"),
    ("now. Long date pattern", "now('D')"),
    ("format", "date_format(registered, 'd')"),
    ("time difference. days > 3", "time_difference('days', now('u'), registered) > `3`"),
    ("time difference. days < 30", "time_difference('days', now('u'), registered) < `30`"),
    ("time difference. days", "time_difference('days', now('u'), registered)"),
    ("time difference. hours", "time_difference('hours', now('u'), registered)"),
    ("time difference. minutes", "time_difference('minutes', now('u'), registered)"),
};

var panel = new Panel(new JsonText(source.EscapeMarkup()));
panel.Header = new PanelHeader("Source");
AnsiConsole.Write(panel);

var table = new Table();
table.Border(TableBorder.Rounded);
table.AddColumn("Example");
table.AddColumn("Expression");
table.AddColumn("Result");

foreach (var (exampleName, expression) in expressions)
{
    var result = parser.Transform(source, expression);
    table.AddRow(
        new Markup(exampleName),
        new Markup(expression.EscapeMarkup()),
        new JsonText(result.EscapeMarkup()));
}

AnsiConsole.Write(table);
