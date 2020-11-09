using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PrvaDomacaZadaca_Kalkulator.Tests {

    [TestClass]
    public class ExampleTests {
        private ICalculator _calculator;

        [TestMethod]
        public void Handle_Display_With_Comma_at_the_end() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('0');
            _calculator.Press(',');

            string displayState = _calculator.GetCurrentDisplayState();
            Assert.AreEqual("0,", displayState);
        }

        [TestMethod]
        public void Handle_Display_With_Multiple_Commas_at_the_end() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('0');
            _calculator.Press(',');
            _calculator.Press(',');

            Assert.AreEqual("0,", _calculator.GetCurrentDisplayState());
        }

        /// <summary>
        /// Provjera piše li nakon "uključivanja" kalkulatora 0 na ekranu
        /// </summary>
        [TestMethod]
        public void CheckDisplay_OnTheBegining_Zero() {
            _calculator = Factory.CreateCalculator();

            string displayState = _calculator.GetCurrentDisplayState();
            Assert.AreEqual("0", displayState);
        }

        /// <summary>
        /// Provjera greške zaokruživanja kod unarnih operacija.
        /// </summary>
        [TestMethod]
        public void CheckDisplay_UnaryOperation_TooLargeResult() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('2');
            _calculator.Press('5');
            _calculator.Press('4');
            _calculator.Press('6');
            _calculator.Press('5');
            _calculator.Press('5');
            _calculator.Press('Q');
            Assert.AreEqual("-E-", _calculator.GetCurrentDisplayState());
            // Dobiveni rezultat ima više od 10 znamenki prije decimalnog znaka
            
            _calculator = Factory.CreateCalculator();
            _calculator.Press('1');
            _calculator.Press('0');
            _calculator.Press('0');
            _calculator.Press('0');
            _calculator.Press('0');
            _calculator.Press('Q');
            Assert.AreEqual("100000000", _calculator.GetCurrentDisplayState());
        }

        /// <summary>
        /// Provjera overflowa kod binarnih operacija.
        /// </summary>
        [TestMethod]
        public void CheckDisplay_BinaryOperation_UpperLimit() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('5');
            _calculator.Press('0');
            _calculator.Press('0');
            _calculator.Press('0');
            _calculator.Press('0');
            _calculator.Press('0');
            _calculator.Press('0');
            _calculator.Press('0');
            _calculator.Press('0');
            _calculator.Press('M');
            Assert.AreEqual("-500000000", _calculator.GetCurrentDisplayState());
            _calculator.Press('*');
            _calculator.Press('2');
            _calculator.Press('=');
            Assert.AreEqual("-1000000000", _calculator.GetCurrentDisplayState());
        }
        
        /// <summary>
        /// Dugi izraz.
        /// </summary>
        [TestMethod]
        public void CheckDisplay_LongOperation() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('2');
            _calculator.Press('+');
            _calculator.Press('I');
            Assert.AreEqual("0,5", _calculator.GetCurrentDisplayState());
            _calculator.Press('5');
            _calculator.Press('6');
            Assert.AreEqual("56", _calculator.GetCurrentDisplayState());
            _calculator.Press('Q');
            Assert.AreEqual("3136", _calculator.GetCurrentDisplayState());
            _calculator.Press('P');
            Assert.AreEqual("3136", _calculator.GetCurrentDisplayState());
            _calculator.Press('C');
            Assert.AreEqual("0", _calculator.GetCurrentDisplayState());
            _calculator.Press('3');
            Assert.AreEqual("3", _calculator.GetCurrentDisplayState());
            _calculator.Press('-');
            _calculator.Press('2');
            _calculator.Press('M');
            Assert.AreEqual("-2", _calculator.GetCurrentDisplayState());
            _calculator.Press('M');
            Assert.AreEqual("2", _calculator.GetCurrentDisplayState());
            _calculator.Press('Q');
            _calculator.Press('*');
            _calculator.Press('2');
            _calculator.Press('=');
            Assert.AreEqual("2", _calculator.GetCurrentDisplayState());
            _calculator.Press('G');
            Assert.AreEqual("3136", _calculator.GetCurrentDisplayState());
            _calculator.Press('Q');
            Assert.AreEqual("9834496", _calculator.GetCurrentDisplayState());
            _calculator.Press('*');
            _calculator.Press(',');
            _calculator.Press('7');
            _calculator.Press('S');
            Assert.AreEqual("0,644217687", _calculator.GetCurrentDisplayState());
            _calculator.Press('=');
            Assert.AreEqual("6335556,266", _calculator.GetCurrentDisplayState());
        }

        /// <summary>
        /// Provjera operacija s istim brojem (2*= --> 2*2)
        /// Provjera operacija s istim brojem (2-= --> 2-2)
        /// Provjera operacija s istim brojem (2/= --> 2/2)
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressEqualAfterAny() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('2');
            _calculator.Press('-');
            _calculator.Press('=');
            string displayState = _calculator.GetCurrentDisplayState();
            Assert.AreEqual("0", displayState);
            _calculator = Factory.CreateCalculator();
            _calculator.Press('2');
            _calculator.Press('/');
            _calculator.Press('=');
            displayState = _calculator.GetCurrentDisplayState();
            Assert.AreEqual("1", displayState);
            _calculator = Factory.CreateCalculator();
            _calculator.Press('2');
            _calculator.Press('*');
            _calculator.Press('=');
            displayState = _calculator.GetCurrentDisplayState();
            Assert.AreEqual("4", displayState);
        }

        /// <summary>
        /// Provjera rada memorije
        /// </summary>
        [TestMethod]
        public void CheckDisplay_MemoryCheck() {
            // Vodeći se prethodnom dilemom u vezi postojanja broja ovo bi trebalo bacit grešku jer još nema 
            // unesenog broja.
            _calculator = Factory.CreateCalculator();
            _calculator.Press(',');
            _calculator.Press('P');
            Assert.AreEqual("0,", _calculator.GetCurrentDisplayState());
            // Provjera rada memorije s decimalnim brojevima
            _calculator.Press('2');
            _calculator.Press(',');
            _calculator.Press('2');
            _calculator.Press('P');
            _calculator.Press('2');
            _calculator.Press('P');
            _calculator.Press('G');
            Assert.AreEqual("0,222", _calculator.GetCurrentDisplayState());
        }

        /// <summary>
        /// Provjera ispisuje li se znamenka nakon pritiska na neki broj
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressDigit_PressedDigit() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('2');

            string displayState = _calculator.GetCurrentDisplayState();
            Assert.AreEqual("2", displayState);
        }

        /// <summary>
        /// pritisak više 0 i provjera nalazi li se samo jedna na ekranu
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressMoreZeros_Zero() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('0');
            _calculator.Press('0');
            _calculator.Press('0');

            string displayState = _calculator.GetCurrentDisplayState();
            Assert.AreEqual("0", displayState);
        }

        /// <summary>
        /// pritisak više znamenki nego što može stati na ekran te provjera je li prikazano samo prvih 10
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressMoreDigitsThenAllowed_FirstDigits() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('1');
            _calculator.Press('2');
            _calculator.Press('3');
            _calculator.Press('4');
            _calculator.Press('5');
            _calculator.Press(',');
            _calculator.Press('6');
            _calculator.Press('7');
            _calculator.Press('8');
            _calculator.Press('9');
            _calculator.Press('1');
            _calculator.Press('2');
            _calculator.Press('3');
            _calculator.Press('4');
            _calculator.Press('M');

            string displayState = _calculator.GetCurrentDisplayState();
            Assert.AreEqual(Math.Round(-12345.67891234, 5).ToString(), displayState);
        }

        [TestMethod]
        public void CheckDisplay_UnaryFun() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('1');
            _calculator.Press('Q');
            Assert.AreEqual("1", _calculator.GetCurrentDisplayState());
            _calculator.Press('I');
            Assert.AreEqual("1", _calculator.GetCurrentDisplayState());
            _calculator.Press('R');
            Assert.AreEqual("1", _calculator.GetCurrentDisplayState());
        }

        /// <summary>
        /// Provjera počisti li se sve nakon restiranja kalkulatora
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressOffOn_Number() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('1');
            _calculator.Press('+');
            _calculator.Press('3');
            Assert.AreEqual("3", _calculator.GetCurrentDisplayState());
            _calculator.Press('O');
            Assert.AreEqual("0", _calculator.GetCurrentDisplayState());
            _calculator.Press('=');

            Assert.AreEqual("0", _calculator.GetCurrentDisplayState());
        }

        /// <summary>
        /// Provjera ostaje li početna nula nakon unosa decimalnog znaka na početku
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressDecimalCharacterOnTheBegining_DecimalNumber() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press(',');
            _calculator.Press('2');

            string displayState = _calculator.GetCurrentDisplayState();
            Assert.AreEqual("0,2", displayState);
        }

        /// <summary>
        /// provjera rada s memorijom. Sprema se uvijek broj zapisan na ekranu a dohvaća zadnji spremljeni broj
        /// </summary>
        [TestMethod]
        public void CheckDisplay_SaveAndGetNumberFromMemoryMoreTimes_LastSavedNumber() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('2');
            _calculator.Press('P');
            _calculator.Press('3');
            _calculator.Press('P');
            _calculator.Press('4');
            Assert.AreEqual("234", _calculator.GetCurrentDisplayState());
            _calculator.Press('P');
            Assert.AreEqual("234", _calculator.GetCurrentDisplayState());
            _calculator.Press('5');
            Assert.AreEqual("2345", _calculator.GetCurrentDisplayState());
            _calculator.Press('G');

            Assert.AreEqual("234", _calculator.GetCurrentDisplayState());
        }

        /// <summary>
        /// Provjera promjene predznaka pozitivnog broja
        /// </summary>
        [TestMethod]
        public void CheckDisplay_ChangeSignOfAPositiveNumber_NegativeNumber() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('2');
            _calculator.Press('M');

            string displayState = _calculator.GetCurrentDisplayState();
            Assert.AreEqual("-2", displayState);
        }

        /// <summary>
        /// provjera pravilnog zaokruživanja decimala)
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressSinus_RoundedNumber() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('1');
            _calculator.Press('2');
            _calculator.Press('3');
            _calculator.Press('4');
            _calculator.Press('5');
            _calculator.Press('S');

            string displayState = _calculator.GetCurrentDisplayState();
            Assert.AreEqual(Math.Round(-0.99377163645568116800870483726536, 9).ToString(), displayState);
        }

        /// <summary>
        /// Provjera oduzimanja dva broja
        /// </summary>
        [TestMethod]
        public void CheckDisplay_SubtractOfTwoNumbers_Subtract() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('9');
            _calculator.Press('4');
            _calculator.Press('2');
            _calculator.Press('7');
            _calculator.Press('8');
            _calculator.Press('2');
            Assert.AreEqual("942782", _calculator.GetCurrentDisplayState());
            _calculator.Press(',');
            Assert.AreEqual("942782,", _calculator.GetCurrentDisplayState());
            _calculator.Press('5');
            Assert.AreEqual("942782,5", _calculator.GetCurrentDisplayState());
            _calculator.Press('-');
            Assert.AreEqual("942782,5", _calculator.GetCurrentDisplayState());
            _calculator.Press('1');
            Assert.AreEqual("1", _calculator.GetCurrentDisplayState());
            _calculator.Press('6');
            Assert.AreEqual("16", _calculator.GetCurrentDisplayState());
            _calculator.Press(',');
            Assert.AreEqual("16,", _calculator.GetCurrentDisplayState());
            _calculator.Press('8');
            Assert.AreEqual("16,8", _calculator.GetCurrentDisplayState());
            _calculator.Press('3');
            Assert.AreEqual("16,83", _calculator.GetCurrentDisplayState());
            _calculator.Press('1');
            Assert.AreEqual("16,831", _calculator.GetCurrentDisplayState());
            _calculator.Press('=');

            Assert.AreEqual("942765,669", _calculator.GetCurrentDisplayState());
        }

        /// <summary>
        /// Provjera oduzimanja dva negativna broja
        /// </summary>
        [TestMethod]
        public void CheckDisplay_SubtractOfTwoNegaitiveNumbers_Subtract() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('4');
            _calculator.Press('2');
            _calculator.Press('7');
            Assert.AreEqual("427", _calculator.GetCurrentDisplayState());
            _calculator.Press('M'); //predznak je moguće dodati u bilo kojem trenutku
            Assert.AreEqual("-427", _calculator.GetCurrentDisplayState());
            _calculator.Press('8');
            _calculator.Press('2');
            Assert.AreEqual("-42782", _calculator.GetCurrentDisplayState());
            _calculator.Press(',');
            Assert.AreEqual("-42782,", _calculator.GetCurrentDisplayState());
            _calculator.Press('5');
            Assert.AreEqual("-42782,5", _calculator.GetCurrentDisplayState());
            _calculator.Press('-');
            Assert.AreEqual("-42782,5", _calculator.GetCurrentDisplayState());
            _calculator.Press('1');
            Assert.AreEqual("1", _calculator.GetCurrentDisplayState());
            _calculator.Press('6');
            Assert.AreEqual("16", _calculator.GetCurrentDisplayState());
            _calculator.Press('M');
            Assert.AreEqual("-16", _calculator.GetCurrentDisplayState());
            _calculator.Press(',');
            Assert.AreEqual("-16,", _calculator.GetCurrentDisplayState());
            _calculator.Press('8');
            Assert.AreEqual("-16,8", _calculator.GetCurrentDisplayState());
            _calculator.Press('3');
            Assert.AreEqual("-16,83", _calculator.GetCurrentDisplayState());
            _calculator.Press('1');
            Assert.AreEqual("-16,831", _calculator.GetCurrentDisplayState());
            _calculator.Press('=');

            Assert.AreEqual("-42765,669", _calculator.GetCurrentDisplayState());
        }

        /// <summary>
        /// Provjera množenja dva broja
        /// </summary>
        [TestMethod]
        public void CheckDisplay_ProductOfTwoNumbers_Product() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('8');
            _calculator.Press('4');
            _calculator.Press('2');
            _calculator.Press('6');
            _calculator.Press(',');
            _calculator.Press('5');
            _calculator.Press('*');
            _calculator.Press('5');
            _calculator.Press('3');
            _calculator.Press(',');
            _calculator.Press('7');
            _calculator.Press('7');
            _calculator.Press('2');
            _calculator.Press('=');

            string displayState = _calculator.GetCurrentDisplayState();
            Assert.AreEqual("453109,758", displayState);
        }

        /// <summary>
        /// Provjera ispisuje li se error u slučaju da je rezultat operacije 
        /// veći od dopuštenog
        /// </summary>
        [TestMethod]
        public void CheckDisplay_ResultIsBiggerThanAllowed_Error() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('1');
            _calculator.Press('2');
            _calculator.Press('3');
            _calculator.Press('4');
            _calculator.Press('5');
            _calculator.Press('6');
            _calculator.Press('7');
            _calculator.Press('8');
            _calculator.Press('9');
            _calculator.Press('0');
            _calculator.Press('*');
            _calculator.Press('1');
            _calculator.Press('2');
            _calculator.Press('3');
            _calculator.Press('=');

            string displayState = _calculator.GetCurrentDisplayState();
            Assert.AreEqual("-E-", displayState);
        }

        /// <summary>
        /// provjera funkcije kvadriranja (+ provjera pravilnog zaokruživanja decimala) - NEPOTREBNO
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressSquare_SquareOfANumber() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('1');
            _calculator.Press('2');
            _calculator.Press('3');
            _calculator.Press(',');
            _calculator.Press('4');
            _calculator.Press('5');
            _calculator.Press('Q');
            _calculator.Press('=');

            string displayState = _calculator.GetCurrentDisplayState();
            Assert.AreEqual("15239,9025", displayState);
        }

        /// <summary>
        /// Provjera obrade pogreške prilikom dijeljenja s 0
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressInversOfTheZero_Error() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('0');
            _calculator.Press('I');

            string displayState = _calculator.GetCurrentDisplayState();
            Assert.AreEqual("-E-", displayState);
        }

        /// <summary>
        /// Provjera obrade znaka jednakosti nakon decimalnog broja koji se može zaokružiti (npr. 2,00)
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressEqualAfterDecimalNumber_NaturalNumber() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('2');
            _calculator.Press(',');
            _calculator.Press('0');
            _calculator.Press('=');

            string displayState = _calculator.GetCurrentDisplayState();
            Assert.AreEqual("2", displayState);
        }

        /// <summary>
        /// Provjera sadržaja na ekranu nakon pritiska binarnog operatora (binarni operator se ne ispisuje na ekranu)
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressBinaryOperatorAfterNumber_Number() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('2');
            _calculator.Press(',');
            _calculator.Press('5');
            _calculator.Press('+');

            string displayState = _calculator.GetCurrentDisplayState();
            Assert.AreEqual("2,5", displayState);
        }

        /// <summary>
        /// Provjera sadržaja na ekranu nakon pritiska binarnog operatora (binarni operator se ne ispisuje na ekranu)
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressBinaryOperatorAfterNumber_NaturalNumber() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('2');
            _calculator.Press(',');
            _calculator.Press('0');
            _calculator.Press('+');

            string displayState = _calculator.GetCurrentDisplayState();
            Assert.AreEqual("2", displayState);
        }

        /// <summary>
        /// Provjera operacija s istim brojem (2+= --> 2+2)
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressEqualAfterPlus_OperationWithTheSameNumber() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('2');

            _calculator.Press('+');
            _calculator.Press('=');

            string displayState = _calculator.GetCurrentDisplayState();
            Assert.AreEqual("4", displayState);
        }

        /// <summary>
        /// Provjera {broj1} {binarni} {unarni} {broj1} = {broj1}{binarni}{broj2}
        /// unarni se izračuna i prikaže ali se ne uzima u obzir u binarnoj operaciji
        /// </summary>
        [TestMethod]
        public void CheckDisplay_PressUnaryOperatorAfterBinaryThenEqual_BinaryOperation() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('2');
            Assert.AreEqual("2", _calculator.GetCurrentDisplayState());
            _calculator.Press('+');
            _calculator.Press('I');
            Assert.AreEqual("0,5", _calculator.GetCurrentDisplayState());
            _calculator.Press('3');
            Assert.AreEqual("3", _calculator.GetCurrentDisplayState());
            _calculator.Press('=');
            Assert.AreEqual("5", _calculator.GetCurrentDisplayState());
        }

        /// <summary>
        /// Provjera raznih operacija i prioriteta operatora
        /// </summary>
        [TestMethod]
        public void CheckDisplay_Operators_Result() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('2');
            Assert.AreEqual("2", _calculator.GetCurrentDisplayState());
            _calculator.Press(',');
            Assert.AreEqual("2,", _calculator.GetCurrentDisplayState());
            _calculator.Press('*'); //provjera uzastopnog unosa različitih binarnih operatora (zadnji se pamti)
            Assert.AreEqual("2", _calculator.GetCurrentDisplayState());
            _calculator.Press('-');
            Assert.AreEqual("2", _calculator.GetCurrentDisplayState());
            _calculator.Press('+');
            Assert.AreEqual("2", _calculator.GetCurrentDisplayState());
            _calculator.Press('3');
            Assert.AreEqual("3", _calculator.GetCurrentDisplayState());
            _calculator.Press('-'); //provjera uzastopnog unosa istog binarnog operatora
            Assert.AreEqual("5", _calculator.GetCurrentDisplayState());
            _calculator.Press('-');
            Assert.AreEqual("5", _calculator.GetCurrentDisplayState());
            _calculator.Press('-');
            Assert.AreEqual("5", _calculator.GetCurrentDisplayState());
            _calculator.Press('2');
            Assert.AreEqual("2", _calculator.GetCurrentDisplayState());
            _calculator.Press('Q');
            Assert.AreEqual("4", _calculator.GetCurrentDisplayState());
            _calculator.Press('Q'); //provjera uzastopnog unosa unarnih operatora (svi se izračunavaju)
            Assert.AreEqual("16", _calculator.GetCurrentDisplayState());
            _calculator.Press('*');
            Assert.AreEqual("-11", _calculator.GetCurrentDisplayState());
            _calculator.Press('2');
            Assert.AreEqual("2", _calculator.GetCurrentDisplayState());
            _calculator.Press('-');
            Assert.AreEqual("-22", _calculator.GetCurrentDisplayState());
            _calculator.Press('3');
            Assert.AreEqual("3", _calculator.GetCurrentDisplayState());
            _calculator.Press('C');
            Assert.AreEqual("0", _calculator.GetCurrentDisplayState());
            _calculator.Press('1');
            _calculator.Press('=');

            Assert.AreEqual("-23", _calculator.GetCurrentDisplayState());
        }

        /// <summary>
        /// Provjera raznih operacija i zaokruživanja
        /// </summary>
        [TestMethod]
        public void CheckDisplay_Operations_Result() {
            _calculator = Factory.CreateCalculator();
            _calculator.Press('2');
            _calculator.Press('S');
            Assert.AreEqual("0,909297427", _calculator.GetCurrentDisplayState());
            _calculator.Press('+');
            _calculator.Press('3');
            _calculator.Press('K');

            Assert.AreEqual("-0,989992497", _calculator.GetCurrentDisplayState());
            _calculator.Press('=');
            Assert.AreEqual("-0,08069507", _calculator.GetCurrentDisplayState());
        }
    }

}