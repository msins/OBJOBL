namespace PrvaDomacaZadaca_Kalkulator.CalculatorOperations.TerminalOperations {

    public class Clear : TerminalOperation {
        public override CalculatorContext Invoke(CalculatorContext ctx) {
            var newCtx = ctx.Copy();
            newCtx.Clear();
            return newCtx;
        }
    }

}