using PrvaDomacaZadaca_Kalkulator.CalculatorOperations;
using PrvaDomacaZadaca_Kalkulator.CalculatorOperations.TerminalOperations;

namespace PrvaDomacaZadaca_Kalkulator.states {

    public class OperandState : AbstractState {
        public OperandState(CalculatorContext context) : base(context) {
        }

        public override ICalculatorState PressedOperand(char digit) {
            if (Helper.DigitsCount(Ctx.Num) >= DigitsLimit) {
                return this;
            }

            if (Ctx.Num.Equals("0") && digit == '0') {
                return this;
            }

            if (digit == DecimalDot) {
                if (Ctx.Num.Contains(DecimalDot.ToString())) {
                    return this;
                }

                if (Ctx.Num.Length == 0) {
                    Ctx.Num += "0";
                }

                Ctx.Num += digit;
                return this;
            }

            Ctx.Num += digit;
            Ctx.Ans = double.Parse(Ctx.Num);
            return this;
        }

        public override CalculatorContext GetContext() => Ctx.Copy();

        protected override ICalculatorState OnReset(Reset reset) {
            return new OperandState(reset.Invoke(Ctx));
        }

        protected override ICalculatorState OnClear(Clear clear) {
            return new OperandState(clear.Invoke(Ctx));
        }

        protected override ICalculatorState OnEquation(Equation equation) {
            return this;
        }

        protected override ICalculatorState OnUnary(UnaryOperation unaryOperation) {
            Ctx.Ans = Helper.InvokeUnary(unaryOperation, Ctx.Ans);
            return this;
        }

        protected override ICalculatorState OnBinary(BinaryOperation binaryOperation) {
            Ctx.Num = Helper.TrimZeros(Ctx.Num);
            return new SecondOperandState(Ctx, binaryOperation);
        }

        protected override ICalculatorState OnSave(Save save) {
            save.Invoke(Ctx);
            return this;
        }

        protected override ICalculatorState OnGet(Get get) {
            get.Invoke(Ctx);
            return this;
        }
    }

}