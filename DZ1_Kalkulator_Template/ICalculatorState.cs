using System;
using PrvaDomacaZadaca_Kalkulator.CalculatorOperations;
using PrvaDomacaZadaca_Kalkulator.CalculatorOperations.TerminalOperations;

namespace PrvaDomacaZadaca_Kalkulator {

    public interface ICalculatorState {
        ICalculatorState PressedOperand(char digit);
        ICalculatorState PressedOperation(IOperation operation);
        CalculatorContext GetContext();
    }

    public abstract class AbstractState : ICalculatorState {
        protected static readonly char DecimalDot = Configuration.GetDecimalSeparator();
        protected static readonly int DigitsLimit = Configuration.GetDigitsLimit();
        protected CalculatorContext Ctx;

        protected AbstractState(CalculatorContext context) => Ctx = context;

        public abstract ICalculatorState PressedOperand(char digit);

        public ICalculatorState PressedOperation(IOperation operation) {
            return operation switch {
                BinaryOperation binaryOperation => OnBinary(binaryOperation),
                UnaryOperation unaryOperation => OnUnary(unaryOperation),
                Equation equation => OnEquation(equation),
                Save save => OnSave(save),
                Get get => OnGet(get),
                Reset reset => OnReset(reset),
                Clear clear => OnClear(clear),
                _ => throw new InvalidOperationException()
            };
        }

        public abstract CalculatorContext GetContext();

        protected abstract ICalculatorState OnReset(Reset reset);
        protected abstract ICalculatorState OnClear(Clear clear);
        protected abstract ICalculatorState OnEquation(Equation equation);
        protected abstract ICalculatorState OnUnary(UnaryOperation unaryOperation);
        protected abstract ICalculatorState OnBinary(BinaryOperation binaryOperation);
        protected abstract ICalculatorState OnSave(Save save);
        protected abstract ICalculatorState OnGet(Get get);
    }

}