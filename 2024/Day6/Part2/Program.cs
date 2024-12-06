using System.Numerics;

string[] lines = File.ReadAllLines("../input.txt");

char[,] map = new char[lines.Length, lines[0].Length];
VisitedTile[,] visitedTiles = new VisitedTile[lines.Length, lines[0].Length];
List<Vector2> possibleObstaclePositions = new List<Vector2>();

Vector2 startPosition = new Vector2();
Vector2 currentPosition = new Vector2();
MovingDirection currentDirection = MovingDirection.Up;

for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        if (lines[i][j] == '^')
        {
            startPosition = new Vector2(i, j);
        }

        map[i, j] = lines[i][j];
    }
}

currentPosition = startPosition;

// Find all possible positions where an obstacle could be placed (basically the positions the guard visits on his default path)
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

    if (IsGuardStillInsideMap(newPosition, map))
    {
        if (map[(int)newPosition.X, (int)newPosition.Y] == '#')
        {
            currentDirection = GetNextDirection(currentDirection);
        }
        else
        {
            currentPosition = newPosition;
            if (!possibleObstaclePositions.Contains(newPosition))
            {
                possibleObstaclePositions.Add(newPosition);
            }
        }
    }
    else
    {
        break;
    }
}

int result = 0;

// Brute force all possible positions where an obstacle could be placed and check if that would cause the guard to be stuck in a loop
foreach (Vector2 obstaclePosition in possibleObstaclePositions)
{
    if (obstaclePosition == startPosition)
    {
        continue;
    }

    // Reset everything to a fresh start
    char[,] newMap = (map.Clone() as char[,]) ?? new char[map.GetLength(0), map.GetLength(1)];
    CleanVisitedTiles();
    currentPosition = startPosition;
    currentDirection = MovingDirection.Up;
    
    newMap[(int)obstaclePosition.X, (int)obstaclePosition.Y] = '#';

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

        if (IsGuardStillInsideMap(newPosition, newMap))
        {
            if (visitedTiles[(int)newPosition.X, (int)newPosition.Y].ContainsVisitedDirection(currentDirection))
            {
                // Stuck in loop since he already visted that same tile with the same moving Direction
                //Console.WriteLine("Stuck in loop with obstacle placed at: " + i + " / " + j);
                result++;
                break;
            }

            if (newMap[(int)newPosition.X, (int)newPosition.Y] == '#')
            {
                visitedTiles[(int)newPosition.X, (int)newPosition.Y].AddVisitedDirection(currentDirection);
                currentDirection = GetNextDirection(currentDirection);
            }
            else
            {
                currentPosition = newPosition;
                visitedTiles[(int)newPosition.X, (int)newPosition.Y].AddVisitedDirection(currentDirection);
            }
        }
        else
        {
            break;
        }
    }
}

Console.WriteLine("Result: " + result);

void CleanVisitedTiles()
{
    for (int i = 0; i < visitedTiles.GetLength(0); i++)
    {
        for (int j = 0; j < visitedTiles.GetLength(1); j++)
        {
            visitedTiles[i, j] = new VisitedTile();
        }
    }
}

bool IsGuardStillInsideMap(Vector2 newPosition, char[,] newMap)
{
    if (newPosition.X < 0 || newPosition.X >= newMap.GetLength(0) || newPosition.Y < 0 || newPosition.Y >= newMap.GetLength(1))
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

// Track for each tile if it was already visited from with a specific moving direction
// If he visits the same tile with the same moving direction again, he is stuck in a loop
class VisitedTile
{
    Dictionary<MovingDirection, bool> VisitedDirections = new Dictionary<MovingDirection, bool>
    {
        { MovingDirection.Up, false },
        { MovingDirection.Down, false },
        { MovingDirection.Left, false },
        { MovingDirection.Right, false }
    };

    public void AddVisitedDirection(MovingDirection direction)
    {
        VisitedDirections[direction] = true;
    }

    public bool ContainsVisitedDirection(MovingDirection direction)
    {
        return VisitedDirections[direction];
    }
}