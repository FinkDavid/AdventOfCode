using System.Numerics;

string[] lines = File.ReadAllLines("../input.txt");

//===============================================================
// PARSING
//===============================================================

int[,] map = new int[lines.Length, lines[0].Length];

for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        map[i, j] = int.Parse(lines[i][j].ToString());
    }
}

//===============================================================
// LOGIC
//===============================================================

int result = 0;

for (int i = 0; i < map.GetLength(0); i++)
{
    for (int j = 0; j < map.GetLength(1); j++)
    {
        if (map[i, j] == 0)
        {
            result += GetTrailScore(new Vector2(i, j), new List<Vector2>());
        }
    }
}

Console.WriteLine("Result: " + result);

int GetTrailScore(Vector2 currentPosition, List<Vector2> foundPaths)
{
    int currentHeight = map[(int)currentPosition.X, (int)currentPosition.Y];

    if (currentHeight == 9)
    {
        if (foundPaths.Contains(currentPosition) == false)
        {
            foundPaths.Add(currentPosition);
        }
        else
        {
            return 0;
        }
    }

    Vector2 newPositionUpwards = new Vector2(currentPosition.X - 1, currentPosition.Y);
    if (IsInWorldBounds(newPositionUpwards) && map[(int)newPositionUpwards.X, (int)newPositionUpwards.Y] == currentHeight + 1)
    {
        GetTrailScore(newPositionUpwards, foundPaths);
    }

    Vector2 newPositionDownwards = new Vector2(currentPosition.X + 1, currentPosition.Y);
    if (IsInWorldBounds(newPositionDownwards) && map[(int)newPositionDownwards.X, (int)newPositionDownwards.Y] == currentHeight + 1)
    {
        GetTrailScore(newPositionDownwards, foundPaths);
    }

    Vector2 newPositionLeftwards = new Vector2(currentPosition.X, currentPosition.Y - 1);
    if (IsInWorldBounds(newPositionLeftwards) && map[(int)newPositionLeftwards.X, (int)newPositionLeftwards.Y] == currentHeight + 1)
    {
        GetTrailScore(newPositionLeftwards, foundPaths);
    }

    Vector2 newPositionRightwards = new Vector2(currentPosition.X, currentPosition.Y + 1);
    if (IsInWorldBounds(newPositionRightwards) && map[(int)newPositionRightwards.X, (int)newPositionRightwards.Y] == currentHeight + 1)
    {
        GetTrailScore(newPositionRightwards, foundPaths);
    }

    return foundPaths.Count;
}

bool IsInWorldBounds(Vector2 position)
{
    return position.X >= 0 && position.X < map.GetLength(0) && position.Y >= 0 && position.Y < map.GetLength(1);
}