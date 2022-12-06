using System.Diagnostics.CodeAnalysis;

namespace Calculator.exceptions;

[ExcludeFromCodeCoverage]
public class UnsupportedOperatorException: CalculatorException
{
    public string OperId { get; }
    
    public UnsupportedOperatorException(string operId)
        : base($"Unsupported operator: {operId}")
    {
        OperId = operId;
    }
}