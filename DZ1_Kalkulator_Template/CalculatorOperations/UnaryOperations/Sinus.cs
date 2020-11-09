using System;

namespace PrvaDomacaZadaca_Kalkulator.CalculatorOperations.UnaryOperations {

    public class Sinus : UnaryOperation {
        public override double Invoke(double num) => Math.Sin(num);
    }

}