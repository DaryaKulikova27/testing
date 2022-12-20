using Calculator;
using Calculator.exceptions;
using MSTestExtensions;
using Telerik.JustMock;

namespace CalculatorTests
{
    [TestClass]
    public class CalculatorParserTest
    {
        private ICalculatorEngine mockEngine;
        private readonly CalculatorParser _parser;

        public CalculatorParserTest()
        {
            mockEngine = Mock.Create<ICalculatorEngine>();
            _parser = new CalculatorParser(mockEngine);
        }

        [TestMethod]
        public void SyntaxLetInteger()
        {
            Mock.Arrange(() => mockEngine.LetVar("a", 6)).MustBeCalled();

            _parser.ReadExpression("let a = 6");

            Mock.Assert(mockEngine); 
        }
        
        [TestMethod]
        public void SyntaxLetIdOfVar()
        {
            Mock.Arrange(() => mockEngine.LetVar("b", "a")).MustBeCalled();

            _parser.ReadExpression("let b = a");

            Mock.Assert(mockEngine); 
        }
        
        [TestMethod]
        public void SyntaxVar()
        {
            Mock.Arrange(() => mockEngine.DeclareVar("a")).MustBeCalled();
            _parser.ReadExpression("var a");
            Mock.Assert(mockEngine);
        }

        [TestMethod]
        public void SyntaxFn3Arg()
        {
            Mock.Arrange(() => mockEngine.DeclareFunc("c", "a", "+", "b")).MustBeCalled();
            _parser.ReadExpression("fn c = a + b");
            Mock.Assert(mockEngine);
        }

        [TestMethod]
        public void SyntaxFn()
        {
            Mock.Arrange(() => mockEngine.DeclareFunc("c", "a")).MustBeCalled();
            _parser.ReadExpression("fn c = a");
            Mock.Assert(mockEngine);
        }

        [TestMethod]
        public void SyntaxPrints()
        {
            Mock.Arrange(() => mockEngine.GetValueOperator("c")).MustBeCalled();
            Mock.Arrange(() => mockEngine.PrintFunctions()).Returns("printFunctionsResult");
            Mock.Arrange(() => mockEngine.PrintVars()).Returns("printVarsResult");
            Assert.AreEqual("printFunctionsResult", _parser.ReadExpression("printfns"));
            Assert.AreEqual("printVarsResult", _parser.ReadExpression("printvars"));
            _parser.ReadExpression("print c");
            Mock.Assert(mockEngine); 
        }
        
        [TestMethod]
        public void SyntaxVarTryToSetValue()
        {
            Extensions.Throws(() => _parser.ReadExpression("var a = 0"), "Invalid expression");
        }
        
        [TestMethod]
        public void SyntaxFnTryToSetLiteral()
        {
            Extensions.Throws(() => _parser.ReadExpression("fn a = 0"), "Invalid expression");
        }
    }
}