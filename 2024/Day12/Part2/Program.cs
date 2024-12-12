using System.Linq.Expressions;
using System.Numerics;

string[] lines = File.ReadAllLines("../input.txt");

char[,] map = new char[lines.Length, lines[0].Length];
char[,] editableMap  = new char[lines.Length, lines[0].Length];
char[,] biggerMap  = new char[lines.Length + 2, lines[0].Length + 2];
List<Area> areas = new List<Area>();

Vector2[] directions = new Vector2[]
{
    new Vector2(0, -1), // left
    new Vector2(0, 1),  // right
    new Vector2(-1, 0), // top
    new Vector2(1, 0)   // bottom
};

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

    foreach (Vector2 position in area.positions)
    {
        biggerMap[(int)position.X + 1, (int)position.Y + 1] = area.symbol;
    }

    // Calculate the outer corners of the area
    foreach (Vector2 position in area.positions)
    {
        CheckAndUpdateCorner((int)position.X + 1, (int)position.Y + 1, -1, -1, secondBiggerMap, '.');
        CheckAndUpdateCorner((int)position.X + 1, (int)position.Y + 1, -1, 1, secondBiggerMap, '.');
        CheckAndUpdateCorner((int)position.X + 1, (int)position.Y + 1, 1, -1, secondBiggerMap, '.');
        CheckAndUpdateCorner((int)position.X + 1, (int)position.Y + 1, 1, 1, secondBiggerMap, '.');
    }

    // Now calculate the inner corners
    for (int i = 1; i < biggerMap.GetLength(0) - 1; i++)
    {
        for (int j = 1; j < biggerMap.GetLength(1) - 1; j++)
        {
            CheckAndUpdateCorner(i, j, -1, -1, secondBiggerMap, area.symbol);
            CheckAndUpdateCorner(i, j, -1, 1, secondBiggerMap, area.symbol);
            CheckAndUpdateCorner(i, j, 1, -1, secondBiggerMap, area.symbol);
            CheckAndUpdateCorner(i, j, 1, 1, secondBiggerMap, area.symbol);
        }
    }

    int sides = 0;
    for (int i = 0; i < secondBiggerMap.GetLength(0); i++)
    {
        for (int j = 0; j < secondBiggerMap.GetLength(1); j++)
        {
            if (int.TryParse(secondBiggerMap[i, j].ToString(), out int number))
            {
                sides += number;
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

void CheckAndUpdateCorner(int x, int y, int offsetX, int offsetY, char[,] secondBiggerMap, char symbol)
{
    if (biggerMap[x + offsetX, y] == symbol && biggerMap[x, y + offsetY] == symbol)
    {
        if (biggerMap[x + offsetX, y + offsetY] == '.')
        {
            if (secondBiggerMap[x + offsetX, y + offsetY] == '.')
            {
                secondBiggerMap[x + offsetX, y + offsetY] = '1';
            }
            else
            {
                int parsedNumber = int.Parse(secondBiggerMap[x + offsetX, y + offsetY].ToString());
                parsedNumber++;
                secondBiggerMap[x + offsetX, y + offsetY] = parsedNumber.ToString()[0];
            }
        }
    }
}

class Area
{
    public char symbol;
    public List<Vector2> positions;

    public Area(char symbol, Vector2 pos)
    {
        this.symbol = symbol;
        positions = new List<Vector2>();
        positions.Add(pos);
    }
}