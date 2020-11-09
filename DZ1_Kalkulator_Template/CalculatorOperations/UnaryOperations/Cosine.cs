using System;

namespace PrvaDomacaZadaca_Kalkulator.CalculatorOperations.UnaryOperations {

    public class Cosine : UnaryOperation {
        public override double Invoke(double num) => Math.Cos(num);
    }

}