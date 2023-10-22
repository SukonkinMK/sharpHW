using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw1
{
    
    public class Labirinth
    {
        

    public static int HasExit(int startI, int startJ, int[,] l)
        {
            int counter = 0;
            if (l[startI, startJ] == 1) return 0;
            else if (l[startI, startJ] == 2) counter++;

            var stack = new Stack<Tuple<int,int>>();
            stack.Push(new(startI, startJ));
            while (stack.Count > 0)
            {
                var tmp = stack.Pop();

                if (l[tmp.Item1, tmp.Item2] == 2) counter++; 

                l[tmp.Item1, tmp.Item2] = 1;

                if (tmp.Item2 - 1 >= 0 && l[tmp.Item1, tmp.Item2 - 1] != 1)
                    stack.Push(new(tmp.Item1, tmp.Item2 - 1)); //влево
                if (tmp.Item2 + 1 < l.GetLength(1) && l[tmp.Item1, tmp.Item2 + 1] != 1)
                    stack.Push(new(tmp.Item1, tmp.Item2 + 1)); //вправо
                if (tmp.Item1 - 1 >= 0 && l[tmp.Item1 - 1, tmp.Item2] != 1)
                    stack.Push(new(tmp.Item1 - 1, tmp.Item2)); //вверх
                if (tmp.Item1 + 1 < l.GetLength(0) && l[tmp.Item1 + 1, tmp.Item2] != 1)
                    stack.Push(new(tmp.Item1 + 1, tmp.Item2)); //вниз
            }
            return counter;
        }
    }
}
