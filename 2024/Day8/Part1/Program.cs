using System.Numerics;

string[] lines = File.ReadAllLines("../input.txt");

char[,] map = new char[lines.Length, lines[0].Length];
char[,] mapWithAntinodes = new char[lines.Length, lines[0].Length];
Dictionary<char, List<Vector2>> antennas = new Dictionary<char, List<Vector2>>();

for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        map[i, j] = lines[i][j];
        mapWithAntinodes[i, j] = '.';

        if (map[i, j] != '.')
        {
            if (antennas.ContainsKey(map[i, j]))
            {
                antennas[map[i, j]].Add(new Vector2(i, j));
            }
            else
            {
                antennas.Add(map[i, j], new List<Vector2>());
                antennas[map[i, j]].Add(new Vector2(i, j));
            }
        }
    }
}

foreach (var antenna in antennas)
{
    for (int i = 0; i < antenna.Value.Count; i++)
    {
        for (int j = i + 1; j < antenna.Value.Count; j++)
        {
            Vector2 diff = antenna.Value[i] - antenna.Value[j];
            Vector2 invertedDiff = diff * -1;
            Vector2 antiNoteOne = antenna.Value[i] + diff;
            Vector2 antiNoteTwo = antenna.Value[j] + invertedDiff;

            if (IsInBounds(antiNoteOne, mapWithAntinodes))
            {
                mapWithAntinodes[(int)antiNoteOne.X, (int)antiNoteOne.Y] = '#';
            }

            if (IsInBounds(antiNoteTwo, mapWithAntinodes))
            {
                mapWithAntinodes[(int)antiNoteTwo.X, (int)antiNoteTwo.Y] = '#';
            }
        }
    }
}

int result = 0;

for (int i = 0; i < mapWithAntinodes.GetLength(0); i++)
{
    for (int j = 0; j < mapWithAntinodes.GetLength(1); j++)
    {
        if (mapWithAntinodes[i, j] == '#')
        {
            result++;
        }
    }
}

Console.WriteLine("Result: " + result);

bool IsInBounds(Vector2 position, char[,] map)
{
    return position.X >= 0 && position.X < map.GetLength(0) && position.Y >= 0 && position.Y < map.GetLength(1);
}