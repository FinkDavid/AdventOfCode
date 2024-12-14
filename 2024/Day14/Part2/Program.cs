using System.Numerics;
using System.Text.RegularExpressions;

string[] lines= File.ReadAllLines("../input.txt");
List<Robot> robots = new List<Robot>();

(int x, int y) mapSize = (101, 103);

//===============================================================
// PARSING
//===============================================================

for (int i = 0; i < lines.Length; i++)
{
    Regex regex = new Regex(@"p=(\d+),(\d+) v=(\-?\d+),(\-?\d+)");
    
    Match match = regex.Match(lines[i]);

    if (match.Success)
    {
        int xPos = int.Parse(match.Groups[1].Value);
        int yPos = int.Parse(match.Groups[2].Value);
        int velX = int.Parse(match.Groups[3].Value);
        int velY = int.Parse(match.Groups[4].Value);
        robots.Add(new Robot(new Vector2(xPos, yPos), new Vector2(velX, velY)));
    }
}

//===============================================================
// LOGIC
//===============================================================

int rounds = 10000;

int minimumResult = int.MaxValue;
int christmasTreeRound = 0;
char[,] map = new char[mapSize.x, mapSize.y];

for (int i = 0; i < rounds; i++)
{
    foreach (Robot robot in robots)
    {
        robot.currentPosition += robot.velocity;

        if (robot.currentPosition.X < 0)
        {
            int diff = Math.Abs((int)robot.currentPosition.X);
            robot.currentPosition.X = mapSize.x - diff;
        }

        if (robot.currentPosition.Y < 0)
        {
            int diff = Math.Abs((int)robot.currentPosition.Y);
            robot.currentPosition.Y = mapSize.y - diff;
        }

        if (robot.currentPosition.X >= mapSize.x)
        {
            int diff = (int)robot.currentPosition.X - mapSize.x;
            robot.currentPosition.X = diff;
        }

        if (robot.currentPosition.Y >= mapSize.y)
        {
            int diff = (int)robot.currentPosition.Y - mapSize.y;
            robot.currentPosition.Y = diff;
        }
    }

    int topLeftQuadrantCount = 0;
    int topRightQuadrantCount = 0;
    int bottomLeftQuadrantCount = 0;
    int bottomRightQuadrantCount = 0;

    foreach (Robot robot in robots)
    {
        if (robot.currentPosition.X < mapSize.x / 2 && robot.currentPosition.Y < mapSize.y / 2)
        {
            topLeftQuadrantCount++;
        }
        else if (robot.currentPosition.X > mapSize.x / 2 && robot.currentPosition.Y < mapSize.y / 2)
        {
            topRightQuadrantCount++;
        }
        else if (robot.currentPosition.X < mapSize.x / 2 && robot.currentPosition.Y > mapSize.y / 2)
        {
            bottomLeftQuadrantCount++;
        }
        else if (robot.currentPosition.X > mapSize.x / 2 && robot.currentPosition.Y > mapSize.y / 2)
        {
            bottomRightQuadrantCount++;
        }
    }

    int result = topLeftQuadrantCount * topRightQuadrantCount * bottomLeftQuadrantCount * bottomRightQuadrantCount;

    if (result < minimumResult)
    {
        minimumResult = result;
        christmasTreeRound = i + 1;
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                bool foundRobot = false;
                foreach (Robot robot in robots)
                {
                    if (robot.currentPosition.X == x && robot.currentPosition.Y == y)
                    {
                        foundRobot = true;
                        map[x, y] = '#';
                        break;
                    }
                }

                if (!foundRobot)
                {
                    map[x, y] = '.';
                }
            }
        }
    }
}

for (int i = 0; i < map.GetLength(0); i++)
{
    for (int j = 0; j < map.GetLength(1); j++)
    {
        Console.Write(map[i, j]);
    }
    Console.WriteLine();
}

Console.WriteLine("Result: " + christmasTreeRound);

class Robot
{
    public Vector2 currentPosition;
    public Vector2 velocity;

    public Robot(Vector2 position, Vector2 velocity)
    {
        this.currentPosition = position;
        this.velocity = velocity;
    }
}