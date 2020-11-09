using System;
using PrvaDomacaZadaca_Kalkulator.CalculatorOperations;
using PrvaDomacaZadaca_Kalkulator.CalculatorOperations.TerminalOperations;

namespace PrvaDomacaZadaca_Kalkulator.states {

    public class ProxySecondOperandState : AbstractState {
        private readonly SecondOperandState _oldState;

        public ProxySecondOperandState(CalculatorContext context, SecondOperandState oldState) : base(context) {
            _oldState = oldState;
        }

        public override ICalculatorState PressedOperand(char digit) => _oldState.PressedOperand(digit);

        public new ICalculatorState PressedOperation2(IOperation operation) => _oldState.PressedOperation(operation);


        public override CalculatorContext GetContext() => Ctx;

        protected override ICalculatorState OnReset(Reset reset) => throw new InvalidOperationException();
        protected override ICalculatorState OnClear(Clear clear) => throw new InvalidOperationException();
        protected override ICalculatorState OnEquation(Equation equation) => throw new InvalidOperationException();
        protected override ICalculatorState OnUnary(UnaryOperation unaryOperation) => throw new InvalidOperationException();
        protected override ICalculatorState OnBinary(BinaryOperation binaryOperation) => throw new InvalidOperationException();
        protected override ICalculatorState OnSave(Save save) => throw new InvalidOperationException();
        protected override ICalculatorState OnGet(Get get) => throw new InvalidOperationException();
    }

}