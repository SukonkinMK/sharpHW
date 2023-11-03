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
        double Sum(int x);
        double Sub(int x);
        double Multy(int x);
        double Divide(int x);
        
        event EventHandler<EventArgs> MyEventHandler;
    }
}
