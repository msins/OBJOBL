using System;

namespace PrvaDomacaZadaca_Kalkulator.CalculatorOperations.UnaryOperations {

    public class Inverse : UnaryOperation {
        public override double Invoke(double num) {
            if (Math.Abs(num) < 10e-9) {
                throw new ArithmeticException("Division by zero.");
            }

            return 1 / num;
        }

    }

}