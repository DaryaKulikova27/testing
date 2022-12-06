using System.Diagnostics.CodeAnalysis;

namespace CalculatorTests;

[ExcludeFromCodeCoverage]
public class Extensions
{
    public static void Throws(Action task, string expectedMessage)
    {
        Exception? exc = null;
        try
        {
            task();
        }
        catch (Exception ex)
        {
            exc = ex;
        }
        if (exc == null)
            Assert.Fail("Expected exception but no exception was thrown.");
        else if (!exc.Message.Contains(expectedMessage))
            Assert.Fail($"Expected exception message to include '{expectedMessage}' but it was '{exc.Message}'");
    }
}