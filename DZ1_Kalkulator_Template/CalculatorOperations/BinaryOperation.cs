namespace PrvaDomacaZadaca_Kalkulator.CalculatorOperations {

    public abstract class BinaryOperation : IOperation {
        public abstract double Invoke(double num1, double num2);
    }

}