using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw1
{
    public class CalculatorFormatException : CalculatorExeption
    {
        public CalculatorFormatException() { }
        public CalculatorFormatException(string message) : base(message) { }
    }
}
