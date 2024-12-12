using System.Numerics;
using System.Security;

string[] lines = File.ReadAllLines("../input.txt");

char[,] map = new char[lines.Length, lines[0].Length];
char[,] editableMap  = new char[lines.Length, lines[0].Length];
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

foreach (Area area in areas)
{
    int perimeter = 0;

    foreach (Vector2 position in area.positions)
    {
        foreach (Vector2 direction in directions)
        {
            Vector2 adjacentTile = new Vector2(position.X + direction.X, position.Y + direction.Y);

            if (!IsInBounds(adjacentTile))
            {
                perimeter++;
                continue;
            }

            if (map[(int)adjacentTile.X, (int)adjacentTile.Y] != area.symbol)
            {
                perimeter++;
            }
        }
    }
    
    result += perimeter * area.positions.Count;
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

        if (!IsInBounds(adjacentTile))
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

bool IsInBounds(Vector2 position)
{
    return position.X >= 0 && position.X < map.GetLength(0) && position.Y >= 0 && position.Y < map.GetLength(1);
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