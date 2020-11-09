using System;
using System.Collections.Generic;
using PrvaDomacaZadaca_Kalkulator.BinaryOperations;
using PrvaDomacaZadaca_Kalkulator.CalculatorOperations;
using PrvaDomacaZadaca_Kalkulator.CalculatorOperations.TerminalOperations;
using PrvaDomacaZadaca_Kalkulator.CalculatorOperations.UnaryOperations;

namespace PrvaDomacaZadaca_Kalkulator {

    public static class Operations {
        private static readonly Dictionary<char, IOperation> OperationsDict;

        static Operations() {
            OperationsDict = new Dictionary<char, IOperation> {
                {'S', new Sinus()},
                {'K', new Cosine()},
                {'T', new Tangent()},
                {'Q', new Square()},
                {'R', new Root()},
                {'I', new Inverse()},
                {'M', new Negation()},
                {'+', new Addition()},
                {'-', new Subtraction()},
                {'*', new Multiplication()},
                {'/', new Division()},
                {'=', new Equation()},
                {'O', new Reset()},
                {'C', new Clear()},
                {'P', new Save()},
                {'G', new Get()}
            };
        }

        public static IOperation From(char operation) {
            if (!OperationsDict.ContainsKey(operation)) {
                throw new ArgumentException($"Invalid operation: {operation}");
            }

            return OperationsDict[operation];
        }
    }

}