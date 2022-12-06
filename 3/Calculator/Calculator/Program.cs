
namespace Calculator;

using System.Diagnostics.CodeAnalysis;
using exceptions;

[ExcludeFromCodeCoverage]
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter the expression below");
        var parser = new CalculatorParser();

        string line;
        while ((line = Console.ReadLine()) != null)
        {
            // Console.Write("> ");
            Console.Out.Flush();
            try
            {
                Console.WriteLine(">> " + parser.ReadExpression(line));
            }
            catch (CalculatorException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
