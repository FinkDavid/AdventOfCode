﻿string[] lines = File.ReadAllLines("../input.txt");

List<int> leftNumbers = new List<int>();
List<int> rightNumbers = new List<int>();

foreach (string line in lines)
{
    leftNumbers.Add(int.Parse(line.Split(' ')[0]));
    rightNumbers.Add(int.Parse(line.Split(' ')[3]));
}

int similarityScore = 0;

for (int i = 0; i < leftNumbers.Count; i++)
{
    int sameRightNumbers = rightNumbers.FindAll(x => x == leftNumbers[i]).Count;
    similarityScore += leftNumbers[i] * sameRightNumbers;
}

Console.WriteLine("Result: " + similarityScore);