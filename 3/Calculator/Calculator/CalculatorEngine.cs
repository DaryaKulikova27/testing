using System.Text;
using Calculator.exceptions;

namespace Calculator
{
    public class CalculatorEngine : ICalculatorEngine
    {
        private interface IExecutable
        {
            double Execute(CalculatorEngine calculatorEngine);
        }

        private class TripleFunction : IExecutable
        {
            private readonly string _operand1;
            private readonly string _operatorName;
            private readonly string _operand2;

            public TripleFunction(string operand1, string operatorName, string operand2)
            {
                _operand1 = operand1;
                _operatorName = operatorName;
                _operand2 = operand2;
            }

            public double Execute(CalculatorEngine calculatorEngine)
            {
                var value1 = calculatorEngine.GetValueOperator(_operand1);
                var value2 = calculatorEngine.GetValueOperator(_operand2);

                if (double.IsNaN(value1) || double.IsNaN(value2))
                {
                    return double.NaN;
                }

                return _operatorName switch
                {
                    "+" => value1 + value2,
                    "-" => value1 - value2,
                    "*" => value1 * value2,
                    "/" => value1 / value2,
                    _ => throw new UnsupportedOperatorException(_operatorName)
                };
            }
        }

        private class SimpleFunction : IExecutable
        {
            private readonly string _varId;

            public SimpleFunction(string varId)
            {
                _varId = varId;
            }

            public double Execute(CalculatorEngine calculatorEngine)
            {
                return calculatorEngine.GetValueOperator(_varId);
            }
        }

        private readonly Dictionary<string, double> _vars = new();
        private readonly Dictionary<string, IExecutable> _funcs = new();

        bool IsNameClaimed(string id)
        {
            return _vars.ContainsKey(id) || _funcs.ContainsKey(id);
        }

        public void DeclareVar(string id)
        {
            if (IsNameClaimed(id))
                throw new IdentifierRedeclarationException(id);
            _vars[id] = double.NaN;
        }

        public void LetVar(string id, double value)
        {
            if (_funcs.ContainsKey(id))
                throw new IdentifierRedeclarationException(id);
            _vars[id] = value;
        }

        public void LetVar(string id, string valueFrom)
        {
            if (!IsNameClaimed(valueFrom))
                throw new IdentifierNotFoundException(valueFrom);
            LetVar(id, GetValueOperator(valueFrom));
        }

        public void DeclareFunc(string id, string valueFrom)
        {
            if (IsNameClaimed(id))
                throw new IdentifierRedeclarationException(id);
            if (!IsNameClaimed(valueFrom))
                throw new IdentifierNotFoundException(valueFrom);
            _funcs[id] = new SimpleFunction(valueFrom);
        }

        public void DeclareFunc(string id, string operand1, string operatorName, string operand2)
        {
            if (IsNameClaimed(id))
                throw new IdentifierRedeclarationException(id);
            if (!IsNameClaimed(operand1))
                throw new IdentifierNotFoundException(operand1);
            if (!IsNameClaimed(operand2))
                throw new IdentifierNotFoundException(operand2);
            _funcs[id] = new TripleFunction(operand1, operatorName, operand2);
        }

        public double GetValueOperator(string id)
        {
            if (_funcs.ContainsKey(id))
                return _funcs[id].Execute(this);
            if (_vars.ContainsKey(id))
                return _vars[id];
            throw new IdentifierNotFoundException(id);
        }

        public string PrintVars()
        {
            var sb = new StringBuilder();
            foreach(var key in _vars.Keys)
            {
                sb.Append(key);
                sb.Append(": ");
                sb.Append(GetValueOperator(key));
                sb.Append('\n');
            }
            return sb.ToString();
        }

        public string PrintFunctions()
        {
            var sb = new StringBuilder();
            foreach(var key in _funcs.Keys)
            {
                sb.Append(key);
                sb.Append(": ");
                sb.Append(GetValueOperator(key));
                sb.Append('\n');
            }
            return sb.ToString();
        }
    }
}