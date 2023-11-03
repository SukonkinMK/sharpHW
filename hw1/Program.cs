var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 11, 12, 13, 14, 15, 16, 17, 18 };
int target = 24;

var set = new HashSet<int>();

for (int i = 0;i < numbers.Length; i++) 
{
	for (int j = i + 1; j < numbers.Length; j++)
	{
		if(set.Contains(target - numbers[i] - numbers[j]))
			Console.WriteLine($"{target} = {numbers[i]} + {numbers[j]} + {target - numbers[i] - numbers[j]}");
		else
			set.Add(numbers[j]);
	}
	set.Clear();
}
Console.ReadLine();