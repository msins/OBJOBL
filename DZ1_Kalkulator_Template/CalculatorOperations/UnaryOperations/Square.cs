using System;

namespace PrvaDomacaZadaca_Kalkulator.CalculatorOperations.UnaryOperations {

    public class Square : UnaryOperation {
        public override double Invoke(double num) => Math.Pow(num, 2);
    }

}