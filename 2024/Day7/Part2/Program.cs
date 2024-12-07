string[] lines = File.ReadAllLines("../input.txt");
List<InputLine> inputLines = new List<InputLine>();

foreach (string line in lines)
{
    string[] parts = line.Split(':');

    InputLine inputLine = new InputLine(long.Parse(parts[0]));
    
    string[] numbersString = parts[1].Split(' ');

    foreach (string numberString in numbersString)
    {
        if (!String.IsNullOrWhiteSpace(numberString))
        {
            inputLine.AddNumber(int.Parse(numberString));
        }
    }

    inputLines.Add(inputLine);
}

long result = 0;

foreach (InputLine inputLine in inputLines)
{
    List<string> combinations = GenerateCombinations(inputLine.numbers.Count - 1);

    for (int i = 0; i < combinations.Count; i++)
    {
        long sum = inputLine.numbers[0];
        for (int j = 0; j < combinations[i].Length; j++)
        {
            if (combinations[i][j] == '*')
            {
                sum *= inputLine.numbers[j + 1];
            }
            else if (combinations[i][j] == '+')
            {
                sum += inputLine.numbers[j + 1];
            }
            else if (combinations[i][j] == '|')
            {
                string sumString = sum.ToString();
                sumString += inputLine.numbers[j + 1].ToString();
                sum = long.Parse(sumString);
            }
        }
        
        if (sum == inputLine.testValue)
        {
            result += sum;
            break;
        }
    }
}

Console.WriteLine("Result: " + result);

List<string> GenerateCombinations(int length)
{
    char[] symbols = { '*', '+', '|'};
    List<string> combinations = new List<string>();
    int totalCombinations = (int)Math.Pow(symbols.Length, length);

    for (int i = 0; i < totalCombinations; i++)
    {
        string combination = "";
        int temp = i;

        for (int j = 0; j < length; j++)
        {
            combination = symbols[temp % symbols.Length] + combination;
            temp /= symbols.Length;
        }

        combinations.Add(combination);
    }

    return combinations;
}

class InputLine
{
    public long testValue;
    public List<int> numbers;

    public InputLine(long testValue)
    {
        numbers = new List<int>();
        this.testValue = testValue;
    }

    public void AddNumber(int number)
    {
        numbers.Add(number);
    }
}