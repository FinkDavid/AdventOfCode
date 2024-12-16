using System.Numerics;

string[] lines = File.ReadAllLines("../input.txt");

//===============================================================
// PARSING
//===============================================================

int mapSizeX = 0;
bool mapPartDone = false;
List<char> movingDirections = new List<char>();
Vector2 robotPosition = new Vector2(0, 0);

for (int i = 0; i < lines.Length; i++)
{
    if (mapPartDone)
    {
        foreach (var direction in lines[i])
        {
            movingDirections.Add(direction);
        }
    }

    if (string.IsNullOrWhiteSpace(lines[i]) && mapPartDone == false)
    {
        mapPartDone = true;
        mapSizeX = i;
    }
}

char[,] map = new char[mapSizeX, lines[0].Length * 2];

for (int i = 0; i < mapSizeX; i++)
{
    int j2 = 0;
    for (int j = 0; j < lines[i].Length; j++)
    {
        if (lines[i][j] == '@')
        {
            robotPosition = new Vector2(i, j2);
            map[i, j2] = '@';
            map[i, j2 + 1] = '.';
        }
        else if (lines[i][j] == 'O')
        {
            map[i, j2] = '[';
            map[i, j2 + 1] = ']';
        }
        else if (lines[i][j] == '.')
        {
            map[i, j2] = '.';
            map[i, j2 + 1] = '.';
        }
        else if (lines[i][j] == '#')
        {
            map[i, j2] = '#';
            map[i, j2 + 1] = '#';
        }

        j2 += 2;
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

//===============================================================
// LOGIC
//===============================================================

foreach (var direction in movingDirections)
{
    switch (direction)
    {
        case '^':
        {
            MoveRobot(-1, 0);
            break;
        }
        case '>':
        {
            MoveRobot(0, 1);
            break;
        }
        case 'v':
        {
            MoveRobot(1, 0);
            break;
        }
        case '<':
        {
            MoveRobot(0, -1);
            break;
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
    Console.WriteLine();
}


for (int i = 0; i < map.GetLength(0); i++)
{
    for (int j = 0; j < map.GetLength(1); j++)
    {
        Console.Write(map[i, j]);
    }
    Console.WriteLine();
}

/*
int result = 0;

for (int i = 0; i < map.GetLength(0); i++)
{
    for (int j = 0; j < map.GetLength(1); j++)
    {
        if (map[i, j] == 'O')
        {
            result += (100 * i) + j;
        }
    }
}

Console.WriteLine("Result: " + result);
*/


void MoveRobot(int xChange, int yChange)
{
    if (map[(int)robotPosition.X + xChange, (int)robotPosition.Y + yChange] == '.')
    {
        map[(int)robotPosition.X, (int)robotPosition.Y] = '.';
        map[(int)robotPosition.X + xChange, (int)robotPosition.Y + yChange] = '@';
        robotPosition = new Vector2(robotPosition.X + xChange, robotPosition.Y + yChange);
    }
    else if (map[(int)robotPosition.X + xChange, (int)robotPosition.Y + yChange] == '[' || map[(int)robotPosition.X + xChange, (int)robotPosition.Y + yChange] == ']')
    {
        Vector2 pos = new Vector2(robotPosition.X + xChange, robotPosition.Y + yChange);
        while (map[(int)pos.X, (int)pos.Y] == '[' || map[(int)pos.X, (int)pos.Y] == ']')
        {
            pos = new Vector2(pos.X + xChange, pos.Y + yChange);
        }

        if (map[(int)pos.X, (int)pos.Y] == '.')
        {
            pos = new Vector2(pos.X + xChange * -1, pos.Y + yChange * -1);
            while (pos != robotPosition)
            {
                map[(int)pos.X + xChange, (int)pos.Y + yChange] = map[(int)pos.X, (int)pos.Y];
                map[(int)pos.X, (int)pos.Y] = '.';
                pos = new Vector2(pos.X + xChange * -1, pos.Y + yChange * -1);
            }

            map[(int)robotPosition.X, (int)robotPosition.Y] = '.';
            map[(int)robotPosition.X + xChange, (int)robotPosition.Y + yChange] = '@';
            robotPosition = new Vector2(robotPosition.X +xChange, robotPosition.Y + yChange);
        }
    }
}