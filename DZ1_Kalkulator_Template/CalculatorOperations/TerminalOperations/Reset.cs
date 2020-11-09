namespace PrvaDomacaZadaca_Kalkulator.CalculatorOperations.TerminalOperations {

    public class Reset : TerminalOperation{
        public override CalculatorContext Invoke(CalculatorContext ctx) {
            var newCtx = ctx.Copy();
            newCtx.Reset();
            return newCtx;
        }
    }

}