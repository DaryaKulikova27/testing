namespace Calculator;

public interface ICalculatorEngine
{
    void DeclareVar(string id);
    void LetVar(string id, double value);
    void LetVar(string id, string valueFrom);
    void DeclareFunc(string id, string valueFrom);
    void DeclareFunc(string id, string operand1, string operatorName, string operand2);
    double GetValueOperator(string id);
    string PrintVars();
    string PrintFunctions();
}