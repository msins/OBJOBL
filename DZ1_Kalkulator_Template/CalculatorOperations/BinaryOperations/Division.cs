using PrvaDomacaZadaca_Kalkulator.CalculatorOperations;

namespace PrvaDomacaZadaca_Kalkulator.BinaryOperations {

    public class Division : BinaryOperation {
        public override double Invoke(double num1, double num2) => num1 / num2;
    }

}