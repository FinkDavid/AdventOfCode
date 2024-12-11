string[] lines = File.ReadAllLines("../input.txt");

//===============================================================
// PARSING
//===============================================================

List<long> stones = new List<long>();

foreach (string line in lines)
{
    string[] parts = line.Split(' ');

    for (int i = 0; i < parts.Length; i++)
    {
        stones.Add(long.Parse(parts[i]));
    }
}

//===============================================================
// LOGIC
//===============================================================

int blinks = 25;

for (int i = 0; i < blinks; i++)
{
    for (int j = stones.Count - 1; j >= 0; j--)
    {
        if (stones[j] == 0)
        {
            stones[j] = 1;
        }
        else if (HasEvenAmountOfDigits(stones[j]))
        {
            string number = stones[j].ToString();
            stones[j] = long.Parse(number.Substring(0, number.Length / 2));
            stones.Insert(j + 1, long.Parse(number.Substring(number.Length / 2)));
        }
        else
        {
            stones[j] *= 2024;
        }
    }
}

Console.WriteLine("Result: " + stones.Count);

bool HasEvenAmountOfDigits(long number)
{
    int count = 0;

    while (number != 0)
    {
        number /= 10;
        count++;
    }

    return count % 2 == 0;
}