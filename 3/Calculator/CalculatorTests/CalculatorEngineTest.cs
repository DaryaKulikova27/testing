using Calculator;

namespace CalculatorTests
{
    [TestClass]
    public class CalculatorEngineTest
    {
        private CalculatorEngine engine = new();

        [TestMethod]
        public void GetValueIdDoesNotExist()
        {
            Extensions.Throws(() => engine.GetValueOperator("notdefined"), "Failed to find identifier");
        }

        [TestMethod]
        public void VarNormal()
        {
            engine.DeclareVar("test");
        }

        [TestMethod]
        public void LetNormalFromLiteral()
        {
            engine.LetVar("test", 123.4);
        }

        [TestMethod]
        public void LetNormalFromId()
        {
            engine.DeclareVar("test2");
            engine.LetVar("test", "test2");
        }

        [TestMethod]
        public void FnNormalFromId()
        {
            engine.DeclareVar("test2");
            engine.DeclareFunc("test", "test2");
        }

        [TestMethod]
        public void FnNormalFromTripleStatementTestOperators()
        {
            engine.LetVar("test2", 4);
            engine.LetVar("test3", 2);
            engine.DeclareFunc("testPlus", "test2", "+", "test3");
            engine.DeclareFunc("testMinus", "test2", "-", "test3");
            engine.DeclareFunc("testMult", "test2", "*", "test3");
            engine.DeclareFunc("testDivide", "test2", "/", "test3");
            Assert.AreEqual(6d, engine.GetValueOperator("testPlus"), double.Epsilon, "Mismatched value from calculator");
            Assert.AreEqual(2d, engine.GetValueOperator("testMinus"), double.Epsilon, "Mismatched value from calculator");
            Assert.AreEqual(8d, engine.GetValueOperator("testMult"), double.Epsilon, "Mismatched value from calculator");
            Assert.AreEqual(2d, engine.GetValueOperator("testDivide"), double.Epsilon, "Mismatched value from calculator");
        }

        [TestMethod]
        public void FnNormalFromTripleStatementTestNaN()
        {
            engine.LetVar("test2", 4);
            engine.DeclareVar("test3");
            engine.DeclareFunc("testPlus", "test2", "+", "test3");
            engine.DeclareFunc("testPlus2", "test3", "+", "test2");
            Assert.IsTrue(Double.IsNaN(engine.GetValueOperator("testPlus")), "Mismatched value from calculator: Not NaN");
            Assert.IsTrue(Double.IsNaN(engine.GetValueOperator("testPlus2")), "Mismatched value from calculator: Not NaN");
        }

        [TestMethod]
        public void VarDuplication()
        {
            engine.DeclareVar("test2");
            Extensions.Throws(() => engine.DeclareVar("test2"), "Failed to redeclare identifier");
        }

        [TestMethod]
        public void LetToFunction()
        {
            engine.DeclareVar("testVar");
            engine.DeclareFunc("testFunc", "testVar");
            Extensions.Throws(() => engine.LetVar("testFunc", "testVar"), "Failed to redeclare identifier");
        }

        [TestMethod]
        public void LetDuplication()
        {
            engine.LetVar("test2", 9);
            engine.LetVar("test2", 9);
        }

        [TestMethod]
        public void FnDuplication()
        {
            engine.LetVar("testFrom", 9);
            engine.LetVar("testFrom2", 9);
            engine.LetVar("test2", 9);
            Extensions.Throws(() => engine.DeclareFunc("test2", "testFrom"), "Failed to redeclare identifier");
            Extensions.Throws(() => engine.DeclareFunc("test2", "testFrom", "+", "testFrom2"), "Failed to redeclare identifier");
        }

        [TestMethod]
        public void FnMissingIdFrom()
        {
            engine.LetVar("testFrom", 9);
            Extensions.Throws(() => engine.DeclareFunc("test2", "missing"), "Failed to find identifier");
        }

        [TestMethod]
        public void FnMissingOperandsFromTripleStatement()
        {
            engine.DeclareVar("test2");
            Extensions.Throws(() => engine.DeclareFunc("test", "test2", "+", "test3"), "Failed to find identifier");
            Extensions.Throws(() => engine.DeclareFunc("test", "test3", "+", "test2"), "Failed to find identifier");
        }

        [TestMethod]
        public void FnUnknownOperatorFromTripleStatement()
        {
            engine.LetVar("test2", 4);
            engine.LetVar("test3", 5);
            engine.DeclareFunc("test", "test2", "9", "test3");
            Extensions.Throws(() => engine.GetValueOperator("test"), "Unsupported operator");
        }

        [TestMethod]
        public void LetFromUnknownId()
        {
            Extensions.Throws(() => engine.LetVar("test", "test2"), "Failed to find identifier");
        }

        [TestMethod]
        public void Let()
        {
            Extensions.Throws(() => engine.LetVar("test", "test2"), "Failed to find identifier");
        }
    }
}