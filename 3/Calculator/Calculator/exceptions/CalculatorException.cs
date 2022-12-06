namespace Calculator.exceptions;

public class CalculatorException: Exception
{
    public CalculatorException(string message)
        : base(message) {}
}