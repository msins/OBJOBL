using System;
using System.Globalization;
using PrvaDomacaZadaca_Kalkulator.CalculatorOperations;
using PrvaDomacaZadaca_Kalkulator.CalculatorOperations.TerminalOperations;

namespace PrvaDomacaZadaca_Kalkulator.states {

    public class SecondOperandState : AbstractState {
        private readonly BinaryOperation _binaryOperation;
        private CalculatorContext _tempCtx;
        private bool _hasNewOperand;

        public SecondOperandState(CalculatorContext context, BinaryOperation binaryOperation) : base(context) {
            _binaryOperation = binaryOperation;
            _tempCtx = CalculatorContext.Empty();
        }

        public override ICalculatorState PressedOperand(char digit) {
            if (Helper.DigitsCount(_tempCtx.Num) >= DigitsLimit) {
                return this;
            }

            if (_tempCtx.Num.Equals("0") && digit == '0') {
                return this;
            }

            if (digit == DecimalDot) {
                if (_tempCtx.Num.Contains(DecimalDot.ToString())) {
                    return this;
                }

                if (_tempCtx.Num.Length == 0) {
                    _tempCtx.Num += "0";
                }

                _hasNewOperand = true;
                _tempCtx.Num += digit;
                return this;
            }

            _hasNewOperand = true;
            _tempCtx.Num += digit;
            _tempCtx.Ans = double.Parse(_tempCtx.Num);
            return this;
        }


        public override CalculatorContext GetContext() => _hasNewOperand ? _tempCtx.Copy() : Ctx.Copy();

        protected override ICalculatorState OnReset(Reset reset) {
            return new OperandState(reset.Invoke(Ctx));
        }

        protected override ICalculatorState OnClear(Clear clear) {
            if (_hasNewOperand) {
                _tempCtx = clear.Invoke(_tempCtx);
                return this;
            }

            return new OperandState(clear.Invoke(Ctx));
        }

        protected override ICalculatorState OnEquation(Equation equation) {
            Ctx.Ans = Helper.InvokeBinary(_binaryOperation, Ctx.Ans, _hasNewOperand ? _tempCtx.Ans : Ctx.Ans);
            return new OperandState(Ctx);
        }

        protected override ICalculatorState OnUnary(UnaryOperation unaryOperation) {
            if (_hasNewOperand) {
                _tempCtx.Ans = Helper.InvokeUnary(unaryOperation, _tempCtx.Ans);
                return this;
            }

            double proxyAns = Helper.InvokeUnary(unaryOperation, Ctx.Ans);
            string proxyNum = Helper.TrimNumber(proxyAns.ToString(CultureInfo.CurrentCulture));
            var proxyCtx = new CalculatorContext(Ctx.GetMemory(), proxyAns, proxyNum);

            return new ProxySecondOperandState(proxyCtx, this);
        }

        protected override ICalculatorState OnBinary(BinaryOperation binaryOperation) {
            if (_hasNewOperand) {
                Ctx.Ans = Helper.InvokeBinary(_binaryOperation, Ctx.Ans, _tempCtx.Ans);
            }

            return new SecondOperandState(Ctx, binaryOperation);
        }

        protected override ICalculatorState OnSave(Save save) {
            if (_hasNewOperand) {
                save.Invoke(_tempCtx);
                Ctx.GetMemory().SetValue(_tempCtx.GetMemory().GetValue());
            }
            else {
                save.Invoke(Ctx);
            }

            return this;
        }

        protected override ICalculatorState OnGet(Get get) => throw new InvalidOperationException();
    }

}