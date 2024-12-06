using System.Numerics;

string[] lines = File.ReadAllLines("../input.txt");

char[,] map = new char[lines.Length, lines[0].Length];

Vector2 currentPosition = new Vector2();
MovingDirection currentDirection = MovingDirection.Up;

for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        if (lines[i][j] == '^')
        {
            currentPosition = new Vector2(i, j);
        }

        map[i, j] = lines[i][j];
    }
}

map[(int)currentPosition.X, (int)currentPosition.Y] = 'X';

while(true)
{
    Vector2 newPosition = currentPosition;

    switch (currentDirection)
    {
        case MovingDirection.Up:
            newPosition.X--;
            break;
        case MovingDirection.Down:
            newPosition.X++;
            break;
        case MovingDirection.Left:
            newPosition.Y--;
            break;
        case MovingDirection.Right:
            newPosition.Y++;
            break;
    }

    if (IsGuardStillInsideMap(newPosition))
    {
        if (map[(int)newPosition.X, (int)newPosition.Y] == '#')
        {
            currentDirection = GetNextDirection(currentDirection);
        }
        else
        {
            currentPosition = newPosition;
            map[(int)currentPosition.X, (int)currentPosition.Y] = 'X';
        }
    }
    else
    {
        break;
    }
}

int result = 0;

for (int i = 0; i < map.GetLength(0); i++)
{
    for (int j = 0; j < map.GetLength(1); j++)
    {
        if (map[i, j] == 'X')
        {
            result++;
        }
    }
}

Console.WriteLine("Result: " + result);

bool IsGuardStillInsideMap(Vector2 newPosition)
{
    if (newPosition.X < 0 || newPosition.X >= map.GetLength(0) || newPosition.Y < 0 || newPosition.Y >= map.GetLength(1))
    {
        return false;
    }

    return true;
}

MovingDirection GetNextDirection(MovingDirection currentDirection)
{
    switch (currentDirection)
    {
        case MovingDirection.Up:
            return MovingDirection.Right;
        case MovingDirection.Right:
            return MovingDirection.Down;
        case MovingDirection.Down:
            return MovingDirection.Left;
        case MovingDirection.Left:
            return MovingDirection.Up;
        default:
            throw new ArgumentOutOfRangeException();
    }
}

enum MovingDirection
{
    Up,
    Down,
    Left,
    Right
}