using System;

namespace PrvaDomacaZadaca_Kalkulator.CalculatorOperations.UnaryOperations {

    public class Tangent : UnaryOperation {
        public override double Invoke(double num) => Math.Tan(num);
    }

}