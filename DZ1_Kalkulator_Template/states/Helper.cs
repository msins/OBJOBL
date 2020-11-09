using System;
using System.Globalization;
using System.Linq;
using PrvaDomacaZadaca_Kalkulator.CalculatorOperations;

namespace PrvaDomacaZadaca_Kalkulator.states {

    internal static class Helper {
        private static readonly int DigitsLimit = Configuration.GetDigitsLimit();
        private static readonly char DecimalSeparator = Configuration.GetDecimalSeparator();

        public static int DigitsCount(string num) {
            return num.Count(char.IsDigit);
        }

        public static string TrimNumber(string num) {
            if (num.Length < DigitsLimit) {
                return num;
            }

            if (num.Count(char.IsDigit) < DigitsLimit) {
                return num;
            }

            //Overflow
            int containsSeparator = num.Contains(DecimalSeparator) ? 1 : 0;
            int containsSign = num.Contains("-") ? 1 : 0;
            if (num.Length > DigitsLimit + containsSeparator + containsSign) {
                throw new ArithmeticException("Number overflow.");
            }

            int count = 0;
            string newNum = string.Concat(
                num.TakeWhile(c => {
                    if (char.IsDigit(c)) {
                        count++;
                    }

                    return count <= DigitsLimit;
                })
            );
            return newNum;
        }

        public static double InvokeBinary(BinaryOperation binaryOperation, double num1, double num2) {
            double ans = Round(binaryOperation.Invoke(num1, num2), DigitsLimit);
            return ans;
        }

        public static double InvokeUnary(UnaryOperation unaryOperation, double num) {
            return Round(unaryOperation.Invoke(num), DigitsLimit);
        }

        public static double Round(double num, int digits) {
            string numStr = num.ToString(CultureInfo.CurrentCulture);
            bool isSigned = numStr.Contains("-");
            if (numStr.Contains(DecimalSeparator)) {
                int decimalSeparatorIdx = numStr.IndexOf(",", StringComparison.Ordinal) - (isSigned ? 1 : 0);
                int roundFormat = digits - decimalSeparatorIdx;
                return Math.Round(num, roundFormat);
            }

            return num;
        }

        public static string TrimZeros(string num) {
            if (num.Contains(",")) {
                num = num.TrimEnd('0');
                num = num.TrimEnd(',');
            }

            return num;
        }
    }

}