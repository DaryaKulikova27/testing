using Calculator;
using Calculator.exceptions;
using MSTestExtensions;

namespace CalculatorTests
{
    [TestClass]
    public class CalculatorParserTest
    {
        private CalculatorParser parser = new();

        [TestMethod]
        public void SyntaxLetInteger()
        {
            parser.ReadExpression("let a = 6");
        }
        
        [TestMethod]
        public void SyntaxLetFloat()
        {
            parser.ReadExpression("let a = 6.5");
        }
        
        [TestMethod]
        public void SyntaxLetIdOfVar()
        {
            parser.ReadExpression("let a = 6.5");
            parser.ReadExpression("let b = a");
        }
        
        [TestMethod]
        public void SyntaxLetIdOfFun()
        {
            parser.ReadExpression("let a = 6.5");
            parser.ReadExpression("fn test = a");
            parser.ReadExpression("let b = test");
        }
        
        [TestMethod]
        public void SyntaxVar()
        {
            parser.ReadExpression("var a");
        }
        
        [TestMethod]
        public void SyntaxFn()
        {
            parser.ReadExpression("let a=6.5");
            parser.ReadExpression("fn b=a");
            parser.ReadExpression("fn c=a+b");
            Assert.AreEqual(13d, (double) parser.ReadExpression("print c"), double.Epsilon, "Mismatched value from calculator");
        }
        
        [TestMethod]
        public void SyntaxPrints()
        {
            parser.ReadExpression("let a = 6");
            parser.ReadExpression("fn b = a");
            parser.ReadExpression("fn c = a + b");
            Assert.AreEqual(12d, (double) parser.ReadExpression("print c"), double.Epsilon, "Mismatched value from calculator");
            Assert.AreEqual("a: 6\n", (string) parser.ReadExpression("printvars"), "Mismatched value from calculator");
            Assert.AreEqual("b: 6\nc: 12\n", (string) parser.ReadExpression("printfns"), "Mismatched value from calculator");
        }
        
        [TestMethod]
        public void SyntaxVarTryToSetValue()
        {
            Extensions.Throws(() => parser.ReadExpression("var a = 0"), "Invalid expression");
        }
        
        [TestMethod]
        public void SyntaxFnTryToSetLiteral()
        {
            Extensions.Throws(() => parser.ReadExpression("fn a = 0"), "Invalid expression");
        }
    }
}