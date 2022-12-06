using System.Diagnostics.CodeAnalysis;

namespace Calculator.exceptions;

[ExcludeFromCodeCoverage]
public class IdentifierRedeclarationException: CalculatorException
{
    public string Id { get; }
    
    public IdentifierRedeclarationException(string id)
        : base($"Failed to redeclare identifier: {id}")
    {
        Id = id;
    }
}