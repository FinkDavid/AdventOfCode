string[] lines = File.ReadAllLines("../input.txt");

//===============================================================
// PARSING
//===============================================================

char[,] map = new char[lines.Length, lines[0].Length];
var start = (0, 0);
var end = (0, 0);

for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        map[i, j] = lines[i][j];
        
        if (lines[i][j] == 'S')
        {
            start = (i, j);
        }

        if (lines[i][j] == 'E')
        {
            end = (i, j);
        }
    }
}

//===============================================================
// LOGIC
//===============================================================

int[] dx = { -1, 1, 0, 0 };
int[] dy = { 0, 0, -1, 1 };
MovingDirection[] directions = { MovingDirection.Up, MovingDirection.Down, MovingDirection.Left, MovingDirection.Right };

int result = FindShortestPath(map, start, end);
Console.WriteLine("Result: " + result);

// A* pathfinding algorithm from Github Copilot (not written by me)
int FindShortestPath(char[,] map, (int x, int y) start, (int x, int y) end)
{
    int rows = map.GetLength(0);
    int cols = map.GetLength(1);
    var pq = new PriorityQueue<(int x, int y, MovingDirection dir, int cost), int>();
    var visited = new HashSet<(int, int, MovingDirection)>();

    foreach (var dir in directions)
    {
        pq.Enqueue((start.x, start.y, dir, 0), 0);
    }

    while (pq.Count > 0)
    {
        var (x, y, dir, cost) = pq.Dequeue();

        if ((x, y) == end)
        {
            return cost;
        }

        if (!visited.Add((x, y, dir)))
        {
            continue;
        }

        for (int i = 0; i < 4; i++)
        {
            int nx = x + dx[i];
            int ny = y + dy[i];
            var newDir = directions[i];
            int newCost = cost + 1 + (dir != newDir ? 1000 : 0);

            if (nx >= 0 && nx < rows && ny >= 0 && ny < cols && map[nx, ny] != '#')
            {
                pq.Enqueue((nx, ny, newDir, newCost), newCost);
            }
        }
    }

    return -1;
}

enum MovingDirection
{
    Up,
    Down,
    Left,
    Right
}