using System.Text.RegularExpressions;

string[] lines = File.ReadAllLines("../input.txt");

string oneLineInput = "";

foreach (string line in lines)
{
    oneLineInput += line;
}

// Removing everything between every don't() until the next do() so we only have the enabled mul instructions left
while (oneLineInput.Contains("don't()"))
{
    int firstIndex = oneLineInput.IndexOf("don't()");
    int secondIndex = oneLineInput.IndexOf("do()", firstIndex);

    if (firstIndex == -1 || secondIndex == -1)
    {
        oneLineInput = oneLineInput.Remove(firstIndex, oneLineInput.Length - firstIndex);
        break;
    }
    else
    {
        oneLineInput = oneLineInput.Remove(firstIndex, secondIndex - firstIndex);
    }
}

Regex regex = new Regex(@"mul\(([0-9]{1,3})\,([0-9]{1,3})\)");
MatchCollection matches = regex.Matches(oneLineInput);

uint result = 0;
foreach (Match match in matches)
{
    result += uint.Parse(match.Groups[1].Value) * uint.Parse(match.Groups[2].Value);
}

Console.WriteLine("Result: " + result);