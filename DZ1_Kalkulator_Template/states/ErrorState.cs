using System;
using PrvaDomacaZadaca_Kalkulator.CalculatorOperations;
using PrvaDomacaZadaca_Kalkulator.CalculatorOperations.TerminalOperations;

namespace PrvaDomacaZadaca_Kalkulator.states {

    public class ErrorState : AbstractState {
        public ErrorState() : base(new CalculatorContext(null, 0, Configuration.GetErrorText())) {
        }

        public ErrorState(CalculatorContext context) : base(context) {
        }

        public override ICalculatorState PressedOperand(char digit) => throw new InvalidOperationException();

        public override CalculatorContext GetContext() => Ctx;

        protected override ICalculatorState OnReset(Reset reset) {
            var newCtx = reset.Invoke(Ctx);
            return new OperandState(newCtx);
        }

        protected override ICalculatorState OnClear(Clear clear) => throw new InvalidOperationException();
        protected override ICalculatorState OnEquation(Equation equation) => throw new InvalidOperationException();
        protected override ICalculatorState OnUnary(UnaryOperation unaryOperation) => throw new InvalidOperationException();
        protected override ICalculatorState OnBinary(BinaryOperation binaryOperation) => throw new InvalidOperationException();
        protected override ICalculatorState OnSave(Save save) => throw new InvalidOperationException();
        protected override ICalculatorState OnGet(Get get) => throw new InvalidOperationException();
    }

}