string[] lines = File.ReadAllLines("../input.txt");

int safeReports = 0;
int maxDistance = 3;

foreach (string line in lines)
{
    List<int> numbers = line.Split(' ').Select(int.Parse).ToList();
    
    if (numbers[1] > numbers[0])
    {
        if (CheckIfSafeReport(numbers, true))
        {
            safeReports++;
        }
    }
    else if (numbers[1] < numbers[0])
    {
        if (CheckIfSafeReport(numbers, false))
        {
            safeReports++;
        }
    }
}

Console.WriteLine("Result: " + safeReports);

bool CheckIfSafeReport(List<int> numbers, bool isIncreasing)
{
    bool isSafeReport = true;
    for (int i = 0; i < numbers.Count - 1; i++)
    {
        if (isIncreasing)
        {
            if (numbers[i + 1] <= numbers[i] || numbers[i + 1] - numbers[i] > maxDistance)
            {
                isSafeReport = false;
                break;
            }
        }
        else
        {
            if (numbers[i + 1] >= numbers[i] || numbers[i] - numbers[i + 1] > maxDistance)
            {
                isSafeReport = false;
                break;
            }
        }
    }

    return isSafeReport;
}