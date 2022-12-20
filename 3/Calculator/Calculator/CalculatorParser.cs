using System.Text.RegularExpressions;
using Calculator.exceptions;

namespace Calculator;

public class CalculatorParser
{
    private static readonly Regex
        RegexPrintvars = new(
            @"^printvars$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase),
        RegexPrintfns = new(
            @"^printfns$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase),
        RegexPrint = new(
            @"^print\s+([a-zA-Z_]+([a-zA-Z0-9_])*)$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase),
        RegexFn = new(
            @"^fn\s+([a-zA-Z_]+([a-zA-Z0-9_])*)\s*=\s*((([a-zA-Z_]+([a-zA-Z0-9_])*)\s*([-|+|*|\/])\s*([a-zA-Z_]+([a-zA-Z0-9_])*))|([a-zA-Z_]+([a-zA-Z0-9_])*))$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase),
        RegexLet = new(
            @"^let\s+([a-zA-Z_]+([a-zA-Z0-9_])*)\s*=\s*(([0-9]*(\.[0-9_]*)?)|([a-zA-Z_]+([a-zA-Z0-9_])*))$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase),
        RegexVar = new(
            @"^var\s+([a-zA-Z_]+([a-zA-Z0-9_])*)$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

    private readonly Dictionary<Regex, Func<Match, object?>> _matchers;

    private readonly ICalculatorEngine _engine;

    public CalculatorParser(ICalculatorEngine engine)
    {
        _engine = engine;
        _matchers = new Dictionary<Regex, Func<Match, object?>>
        {
            {RegexPrintvars, ExprPrintVars},
            {RegexPrintfns, ExprPrintFns},
            {RegexPrint, ExprPrint},
            {RegexFn, ExprFn},
            {RegexLet, ExprLet},
            {RegexVar, ExprVar}
        };
    }

    public CalculatorParser() :
        this(new CalculatorEngine()) { }

    public object? ReadExpression(string expression)
    {
        expression = expression.Trim();
        foreach (var pair in _matchers)
        {
            var matches = pair.Key.Matches(expression);
            if (matches.Count != 1) continue;
            return pair.Value(matches[0]);
        }

        throw new InvalidExpressionException(expression);
    }

    private object ExprPrintVars(Match match)
    {
        return _engine.PrintVars();
    }

    private object ExprPrintFns(Match match)
    {
        return _engine.PrintFunctions();
    }

    private object? ExprPrint(Match match)
    {
        return _engine.GetValueOperator(match.Groups[1].Value);
    }

    private object? ExprFn(Match match)
    {
        if (string.IsNullOrEmpty(match.Groups[8].Value)) // simple eq
            _engine.DeclareFunc(match.Groups[1].Value, match.Groups[10].Value);
        else // triple stmt
            _engine.DeclareFunc(match.Groups[1].Value, match.Groups[5].Value, match.Groups[7].Value,
                match.Groups[8].Value);
        return null;
    }

    private object? ExprLet(Match match)
    {
        if (string.IsNullOrEmpty(match.Groups[6].Value)) // simple eq
            _engine.LetVar(match.Groups[1].Value, Double.Parse(match.Groups[4].Value));
        else // triple stmt
            _engine.LetVar(match.Groups[1].Value, match.Groups[6].Value);
        return null;
    }

    private object? ExprVar(Match match)
    {
        _engine.DeclareVar(match.Groups[1].Value);
        return null;
    }
}