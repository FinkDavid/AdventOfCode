using System.Numerics;

string[] lines = File.ReadAllLines("../input.txt");

char[,] map = new char[lines.Length, lines[0].Length];
char[,] editableMap  = new char[lines.Length, lines[0].Length];
char[,] biggerMap  = new char[lines.Length + 2, lines[0].Length + 2];
List<Area> areas = new List<Area>();

Vector2[] directions =
[
    new Vector2(0, -1), // left
    new Vector2(0, 1),  // right
    new Vector2(-1, 0), // top
    new Vector2(1, 0)   // bottom
];

Vector2[] diagonals =
[
    new Vector2(-1, -1),
    new Vector2(-1, 1),
    new Vector2(1, -1),
    new Vector2(1, 1)
];

//===============================================================
// PARSING
//===============================================================

for (int i = 0; i < biggerMap.GetLength(0); i++)
{
    for (int j = 0; j < biggerMap.GetLength(1); j++)
    {
        biggerMap[i, j] = '.';
    }
}

for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        editableMap[i, j] = lines[i][j];
        map[i, j] = lines[i][j];
    }
}

//===============================================================
// LOGIC
//===============================================================

// Creating areas
for (int i = 0; i < editableMap.GetLength(0); i++)
{
    for (int j = 0; j < editableMap.GetLength(1); j++)
    {
        if (editableMap[i, j] == '.')
        {
            continue;
        }
        
        Area foundArea = SearchArea(new Vector2(i, j));
        areas.Add(foundArea);

        foreach (Vector2 position in foundArea.positions)
        {
            editableMap[(int)position.X, (int)position.Y] = '.';
        }
    }
}

int result = 0;

// Counting corners for each area (The amount of corners is equal to the amount of sides)
foreach (Area area in areas)
{
    char[,] secondBiggerMap = new char[biggerMap.GetLength(0), biggerMap.GetLength(1)];
    for (int i = 0; i < biggerMap.GetLength(0); i++)
    {
        for (int j = 0; j < biggerMap.GetLength(1); j++)
        {
            biggerMap[i, j] = '.';
            secondBiggerMap[i, j] = '.';
        }
    }

    // Put the area onto an empty map surrounded by at least one empty space
    foreach (Vector2 position in area.positions)
    {
        biggerMap[(int)position.X + 1, (int)position.Y + 1] = area.symbol;
    }


    int sides = 0;
    // Calculate the outer corners of the area
    foreach (Vector2 position in area.positions)
    {
        foreach (Vector2 diagonal in diagonals)
        {
            if (CheckAndUpdateCorner((int)position.X + 1, (int)position.Y + 1, (int)diagonal.X, (int)diagonal.Y, secondBiggerMap, '.'))
            {
                sides++;
            }
        }
    }

    // Now calculate the inner corners
    for (int i = 1; i < biggerMap.GetLength(0) - 1; i++)
    {
        for (int j = 1; j < biggerMap.GetLength(1) - 1; j++)
        {
            foreach (Vector2 diagonal in diagonals)
            {
                if (CheckAndUpdateCorner(i, j, (int)diagonal.X, (int)diagonal.Y, secondBiggerMap, area.symbol))
                {
                    sides++;
                }
            }
        }
    }
    
    result += sides * area.positions.Count;
}

Console.WriteLine("Result: " + result);

Area SearchArea(Vector2 currentPosition)
{
    Area area = new Area(map[(int)currentPosition.X, (int)currentPosition.Y], currentPosition);
    ContinueSearch(currentPosition, area);
    return area;
}

void ContinueSearch(Vector2 currentPosition, Area currentArea)
{
    foreach (Vector2 direction in directions)
    {
        Vector2 adjacentTile = new Vector2(currentPosition.X + direction.X, currentPosition.Y + direction.Y);

        if (!IsInBounds(adjacentTile, map))
        {
            continue;
        }

        if (map[(int)adjacentTile.X, (int)adjacentTile.Y] == currentArea.symbol)
        {
            if (!currentArea.positions.Contains(adjacentTile))
            {
                currentArea.positions.Add(adjacentTile);
                ContinueSearch(adjacentTile, currentArea);
            }
        }
    }
}

bool IsInBounds(Vector2 position, char[,] map)
{
    return position.X >= 0 && position.X < map.GetLength(0) && position.Y >= 0 && position.Y < map.GetLength(1);
}

bool CheckAndUpdateCorner(int x, int y, int offsetX, int offsetY, char[,] secondBiggerMap, char symbol)
{
    if (biggerMap[x + offsetX, y] == symbol && biggerMap[x, y + offsetY] == symbol)
    {
        if (biggerMap[x + offsetX, y + offsetY] == '.')
        {
            return true;
        }
    }

    return false;
}

class Area
{
    public char symbol;
    public List<Vector2> positions;

    public Area(char symbol, Vector2 pos)
    {
        this.symbol = symbol;
        positions = [pos];
    }
}