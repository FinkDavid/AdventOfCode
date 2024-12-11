string[] lines = File.ReadAllLines("../input.txt");

//===============================================================
// PARSING
//===============================================================

Dictionary<long, long> stoneAppearances = new Dictionary<long, long>();

foreach (string line in lines)
{
    string[] parts = line.Split(' ');

    for (int i = 0; i < parts.Length; i++)
    {
        stoneAppearances.Add(int.Parse(parts[i]), 1);
    }
}

//===============================================================
// LOGIC
//===============================================================

int blinks = 75;

for (int i = 0; i < blinks; i++)
{
    Dictionary<long, long> newStoneAppearances = new Dictionary<long, long>();

    foreach (var stoneAppearance in stoneAppearances)
    {
        if (stoneAppearance.Value == 0)
        {
            continue;
        }

        if (stoneAppearance.Key == 0)
        {
            newStoneAppearances[1] = newStoneAppearances.GetValueOrDefault(1) + stoneAppearance.Value;
        }
        else if (HasEvenAmountOfDigits(stoneAppearance.Key))
        {
            string number = stoneAppearance.Key.ToString();

            long firstHalf = long.Parse(number.Substring(0, number.Length / 2));
            long secondHalf = long.Parse(number.Substring(number.Length / 2));

            newStoneAppearances[firstHalf] = newStoneAppearances.GetValueOrDefault(firstHalf) + stoneAppearance.Value;
            newStoneAppearances[secondHalf] = newStoneAppearances.GetValueOrDefault(secondHalf) + stoneAppearance.Value;
        }
        else
        {
            newStoneAppearances[stoneAppearance.Key * 2024] = newStoneAppearances.GetValueOrDefault(stoneAppearance.Key * 2024) + stoneAppearance.Value;
        }
    }

    stoneAppearances = new Dictionary<long, long>(newStoneAppearances);
}

Console.WriteLine("Result: " + stoneAppearances.Sum(x => x.Value));

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