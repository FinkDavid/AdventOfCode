using System.Text.RegularExpressions;

string[] lines = File.ReadAllLines("../input.txt");

string oneLineInput = "";

foreach (string line in lines)
{
    oneLineInput += line;
}

Regex regex = new Regex(@"mul\(([0-9]{1,3})\,([0-9]{1,3})\)");
MatchCollection matches = regex.Matches(oneLineInput);

uint result = 0;
foreach (Match match in matches)
{
    result += uint.Parse(match.Groups[1].Value) * uint.Parse(match.Groups[2].Value);
}

Console.WriteLine("Result: " + result);