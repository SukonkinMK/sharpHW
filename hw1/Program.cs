using hw1;

var calc = new Calc();
calc.MyEventHandler += Calc_MyEventHandler;

var map = new Dictionary<string, Action<int>>();
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
            if(number != null && int.TryParse(number, out int num))
            {
                map[command](num);
            }
            else
                Console.WriteLine("Ошибка ввода числа");
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