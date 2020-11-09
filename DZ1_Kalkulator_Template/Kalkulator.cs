using System;
using System.Globalization;
using System.Threading;
using PrvaDomacaZadaca_Kalkulator.states;

namespace PrvaDomacaZadaca_Kalkulator {

    public class Factory {
        public static ICalculator CreateCalculator() {
            return new Kalkulator();
        }
    }

    public class Kalkulator : ICalculator {
        private ICalculatorState _state = new OperandState(CalculatorContext.Empty());
        private string _displayState = "0";

        public Kalkulator() {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("hr-HR");
        }

        public void Press(char inPressedDigit) {
            _state = GetNewState(_state, inPressedDigit);
            _displayState = new Display(_state).Format();
        }

        public string GetCurrentDisplayState() {
            return _displayState;
        }

        private static ICalculatorState GetNewState(ICalculatorState state, char inPressedDigit) {
            try {
                if (LexingHelper.IsNumber(inPressedDigit)) {
                    return state.PressedOperand(inPressedDigit);
                }

                return state.PressedOperation(Operations.From(inPressedDigit));
            }
            catch (Exception e) {
                //log
                return new ErrorState();
            }
        }
    }

    internal class Display {
        private string _num;

        public Display(ICalculatorState state) {
            _num = state.GetContext().Num;
        }

        public string Format() {
            if (_num.Length == 0) {
                _num = "0";
            }

            return _num;
        }
    }

    internal static class LexingHelper {
        public static bool IsNumber(char digit) {
            return char.IsDigit(digit) || digit == Configuration.GetDecimalSeparator();
        }
    }

}