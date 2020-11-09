using System;
using System.Globalization;
using PrvaDomacaZadaca_Kalkulator.states;

namespace PrvaDomacaZadaca_Kalkulator {

    public class CalculatorContext {
        private Memory _memory;
        private const double DefaultAns = 0;
        private const string DefaultNum = "";
        private double _ans;

        public CalculatorContext(Memory memory, double ans, string num) {
            _memory = memory;
            _ans = ans;
            Num = num;
        }

        public double Ans {
            get => _ans;
            set {
                _ans = value;
                Num = Helper.TrimNumber(Ans.ToString(CultureInfo.CurrentCulture));
            }
        }

        public string Num { get; set; }

        public static CalculatorContext Empty() {
            return new CalculatorContext(Memory.Empty(), DefaultAns, DefaultNum);
        }

        public bool ContainsMemory() => _memory.IsDefined();
        public Memory GetMemory() => _memory;

        public CalculatorContext Copy() {
            return new CalculatorContext(_memory.Copy(), Ans, Num);
        }

        public void Reset() {
            _memory = Memory.Empty();
            Ans = DefaultAns;
            Num = DefaultNum;
        }

        public void Clear() {
            Num = DefaultNum;
        }
    }

    public class Memory {
        private double? _value;

        private Memory(double? value) {
            _value = value;
        }

        public double GetValue() {
            if (_value == null) {
                throw new InvalidOperationException("Memory not defined.");
            }

            return _value.Value;
        }

        public void SetValue(double? value) {
            _value = value;
        }

        public Memory Copy() {
            return new Memory(_value);
        }

        public static Memory Empty() {
            return new Memory(null);
        }

        public bool IsDefined() => _value != null;
    }

}