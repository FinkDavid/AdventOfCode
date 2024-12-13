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

    List<Vector2d> positions = new List<Vector2d>();

    foreach (Match match in matches)
    {
        if (match.Success)
        {
            positions.Add(new Vector2d(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)));
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

long result = 0;

foreach (ClawMachine clawMachine in clawMachines)
{
    // Solved by calculating the following equations by elimination
    // First equation firstA * x + secondA * y = thirdA
    // Second equation firstB * x + secondB * y = thirdB
    // x is the amount of times button A has to be pressed, y is the amount of times button B has to be pressed

    // First make the coefficients of y in both equations equal by multiplying the equations with the other equation's y coefficient
    double firstA = clawMachine.buttonA.X * clawMachine.buttonB.Y;
    double secondA = clawMachine.buttonB.X * clawMachine.buttonB.Y;
    double thirdA = clawMachine.prizePosition.X * clawMachine.buttonB.Y;
    
    double firstB = clawMachine.buttonA.Y * clawMachine.buttonB.X;
    double secondB = clawMachine.buttonB.Y * clawMachine.buttonB.X;
    double thirdB = clawMachine.prizePosition.Y * clawMachine.buttonB.X;

    // Substract the second equation from the first one to eliminate the y factor and only have x as the only unknown variable left
    double first = firstA - firstB;
    double second = secondA - secondB;
    double third = thirdA - thirdB;

    // Calculate x
    double solutionX = third / first;

    // Calculate y by substituting x into the first equation
    double solutionY = (thirdA - (firstA * solutionX)) / secondA;

    // If the solutions are whole numbers its possible to reach the prize position
    if (solutionX == Math.Floor(solutionX) && solutionY == Math.Floor(solutionY))
    {
        result += (long)solutionX * 3 + (long)solutionY;
    }
}

Console.WriteLine("Result: " + result);

class ClawMachine
{
    public Vector2d buttonA;
    public Vector2d buttonB;
    public Vector2d prizePosition;

    public ClawMachine(Vector2d buttonA, Vector2d buttonB, Vector2d prizePosition)
    {
        this.buttonA = buttonA;
        this.buttonB = buttonB;
        this.prizePosition = new Vector2d(prizePosition.X + 10000000000000, prizePosition.Y + 10000000000000);
    }
}

class Vector2d
{
    public double X;
    public double Y;

    public Vector2d(double x, double y)
    {
        X = x;
        Y = y;
    }
}