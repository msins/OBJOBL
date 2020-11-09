using System;

namespace PrvaDomacaZadaca_Kalkulator.CalculatorOperations.UnaryOperations {

    public class Root : UnaryOperation {
        public override double Invoke(double num) => Math.Sqrt(num);
    }

}