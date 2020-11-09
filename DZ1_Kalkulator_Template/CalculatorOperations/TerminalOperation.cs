namespace PrvaDomacaZadaca_Kalkulator.CalculatorOperations {

    public abstract class TerminalOperation : IOperation {
        public abstract CalculatorContext Invoke(CalculatorContext ctx);
    }

}