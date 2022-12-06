using System.Diagnostics.CodeAnalysis;

namespace Calculator.exceptions;

[ExcludeFromCodeCoverage]
public class IdentifierNotFoundException: CalculatorException
{
    public string Id { get; }

    public IdentifierNotFoundException(string id)
        : base($"Failed to find identifier: {id}")
    {
        Id = id;
    }
}