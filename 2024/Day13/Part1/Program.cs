using System.Numerics;
using System.Text.RegularExpressions;

string[] lines = File.ReadAllLines("../input.txt");
List<ClawMachine> clawMachines = new List<ClawMachine>();

//===============================================================
// PARSING
//===============================================================

for (int i = 0; i < lines.Length; i+=4)
{
    Regex regexButtons = new Regex(@"X\+(\d+), Y\+(\d+)");
    Regex regexPrize = new Regex(@"X=(\d+),\sY=(\d+)");
    
    List<Match> matches =
    [
        regexButtons.Match(lines[i]),
        regexButtons.Match(lines[i + 1]),
        regexPrize.Match(lines[i + 2]),
    ];

    List<Vector2> positions = new List<Vector2>();

    foreach (Match match in matches)
    {
        if (match.Success)
        {
            positions.Add(new Vector2(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)));
        }
    }

    if (positions.Count == 3)
    {
        clawMachines.Add(new ClawMachine(positions[0], positions[1], positions[2]));
    }
}

//===============================================================
// LOGIC
//===============================================================

int result = 0;

foreach (ClawMachine clawMachine in clawMachines)
{
    int minimumCost = int.MaxValue;
    for(int i = 1; i <= 100; i++)
    {
        for(int j = 1; j <= 100; j++)
        {
            int combinedPositionX = (int)(clawMachine.buttonA.X * i + clawMachine.buttonB.X * j);
            int combinedPositionY = (int)(clawMachine.buttonA.Y * i + clawMachine.buttonB.Y * j);

            if (combinedPositionX == clawMachine.prizePosition.X && combinedPositionY == clawMachine.prizePosition.Y)
            {
                minimumCost = i * 3 + j;
            }
        }
    }

    if (minimumCost != int.MaxValue)
    {
        result += minimumCost;
    }
}

Console.WriteLine("Result: " + result);

class ClawMachine
{
    public Vector2 buttonA;
    public Vector2 buttonB;
    public Vector2 prizePosition;

    public ClawMachine(Vector2 buttonA, Vector2 buttonB, Vector2 prizePosition)
    {
        this.buttonA = buttonA;
        this.buttonB = buttonB;
        this.prizePosition = prizePosition;
    }
}