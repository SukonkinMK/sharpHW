using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw1
{
    public class Calc
    {
        public double Result { get; set; } = 0D;
        private Stack<double> LastResult { get; set; } = new Stack<double>();

        public event EventHandler<EventArgs> MyEventHandler;

        private void PrintResult()
        {
            MyEventHandler?.Invoke(this, new EventArgs());
        }


        public void Divide(string x)
        {
            if (DoubleTryPars(x, out double res))
            {
                CheckNegative(res);
                if (res == 0) 
                {
                    throw new CalculatorDivideByZeroException("Деление на ноль");
                }
                Result /= res;
                PrintResult();
                LastResult.Push(Result);
            }
            else
                throw new CalculatorFormatException("Ведено не число");
            
        }

        public void Multy(string x)
        {
            if (DoubleTryPars(x, out double res))
            {
                CheckNegative(res);
                Result *= res;
                PrintResult();
                LastResult.Push(Result);
            }
            else
                throw new CalculatorFormatException("Ведено не число");
        }

        public void Sub(string x)
        {
            if (DoubleTryPars(x, out double res))
            {
                CheckNegative(res);
                Result -= res;
                if(Result < 0)
                {
                    Result += res;
                    throw new CalculatorExeption("При разности получилось отрицательное число. Операция отменена");
                }
                PrintResult();
                LastResult.Push(Result);
            }
            else
                throw new CalculatorFormatException("Ведено не число");
        }

        public void Sum(string x)
        {
            if (DoubleTryPars(x, out double res))
            {
                CheckNegative(res);
                Result += res;
                PrintResult();
                LastResult.Push(Result);
            }
            else
                throw new CalculatorFormatException("Ведено не число");
        }
        public void CancelLast()
        {
            if (LastResult.TryPop(out double res))
            {
                if (LastResult.TryPeek(out double last))
                    Result = last;
                else
                    Result = 0;
                Console.WriteLine("Последнее действие отменено. Результат равен:");
                PrintResult();
            }
            else
            {
                Console.WriteLine("Невозможно отменить послдеднее действие!");
            }
        }

        private bool DoubleTryPars(string str, out double res)
        {
            res = 0;
            if (double.TryParse(str, out double result))
            {
                res = result;
                return true;
            }
            else
                return false;
        }
        private void CheckNegative(double x)
        {
            if (x < 0)
                throw new CalculatorExeption("Введен отрицательный аргумент");
        }
    }
}
