﻿string[] lines = File.ReadAllLines("../input.txt");

List<int> leftNumbers = new List<int>();
List<int> rightNumbers = new List<int>();

foreach (string line in lines)
{
    leftNumbers.Add(int.Parse(line.Split(' ')[0]));
    rightNumbers.Add(int.Parse(line.Split(' ')[3]));
}

int totalDistances = 0;

for (int i = 0; i < leftNumbers.Count; i++)
{
    int smallestLeftNumber = leftNumbers.Min();
    int smallestRightNumber = rightNumbers.Min();

    totalDistances += Math.Abs(smallestLeftNumber - smallestRightNumber);

    leftNumbers.Remove(smallestLeftNumber);
    rightNumbers.Remove(smallestRightNumber);
}

Console.WriteLine("Result: " + totalDistances);