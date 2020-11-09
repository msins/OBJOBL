﻿using PrvaDomacaZadaca_Kalkulator.CalculatorOperations;

namespace PrvaDomacaZadaca_Kalkulator.BinaryOperations {

    public class Addition : BinaryOperation {
        public override double Invoke(double num1, double num2) => num1 + num2;
    }

}