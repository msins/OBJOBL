namespace PrvaDomacaZadaca_Kalkulator.CalculatorOperations {

    public class Save : IOperation {
        public void Invoke(CalculatorContext ctx) {
            ctx.GetMemory().SetValue(ctx.Ans);
        }
    }

}