using System.Diagnostics.CodeAnalysis;

namespace Calculator.exceptions;

[ExcludeFromCodeCoverage]
public class InvalidExpressionException: CalculatorException
{
    public string Expr { get; }
    
    public InvalidExpressionException(string expr)
        : base($"Invalid expression: {expr}")
    {
        Expr = expr;
    }
}