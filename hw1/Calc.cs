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


        public void Divide(int x)
        {
            Result /= x;
            PrintResult();
            LastResult.Push(Result);
        }

        public void Multy(int x)
        {
            Result *= x;
            PrintResult();
            LastResult.Push(Result);
        }

        public void Sub(int x)
        {
            Result -= x;
            PrintResult();
            LastResult.Push(Result);
        }

        public void Sum(int x)
        {
            Result += x;
            PrintResult();
            LastResult.Push(Result);
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
    }
}
