using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw1
{
    public interface ICalc
    {
        double Result { get; set; }
        double Sum(string x);
        double Sub(string x);
        double Multy(string x);
        double Divide(string x);
        
        event EventHandler<EventArgs> MyEventHandler;
    }
}
