using hw1;

var calc = new Calc();
calc.MyEventHandler += Calc_MyEventHandler;

var map = new Dictionary<string, Action<string>>();
map.Add("+", calc.Sum);
map.Add("-", calc.Sub);
map.Add("*", calc.Multy);
map.Add("/", calc.Divide);

Console.WriteLine("Результат = 0");
while (true)
{
    Console.Write("Введите действие. Доступны операции: +,-,*,/. Для выхода введите exit: ");
    string? command = Console.ReadLine();

    if(command != null && command.Length != 0)
    {
        if (map.ContainsKey(command))
        {
            Console.Write("Введите целое число для операции: ");
            string? number = Console.ReadLine();
            if(number != null && number.Length != 0)
            {
                try 
                { 
                    map[command](number); 
                }
                catch (CalculatorFormatException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                catch (CalculatorDivideByZeroException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                catch (CalculatorExeption ex)
                {
                    Console.WriteLine(ex.ToString());
                }                
            }
            else
                Console.WriteLine("Ошибка, введена пустая строка");
        }
        else if(command.Equals("exit"))
        {
            Console.WriteLine("Завершение работы");
            break;
        }
        else
            Console.WriteLine("Неверная команда");
    }
}


void Calc_MyEventHandler(object? sender, EventArgs e)
{
    if (sender is Calc)
        Console.WriteLine(((Calc)sender).Result);
}