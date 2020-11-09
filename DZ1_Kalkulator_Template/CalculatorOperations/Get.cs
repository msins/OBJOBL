namespace PrvaDomacaZadaca_Kalkulator.CalculatorOperations {

    public class Get : IOperation {
        public void Invoke(CalculatorContext ctx) {
            ctx.Ans = ctx.GetMemory().GetValue();
        }
    }

}