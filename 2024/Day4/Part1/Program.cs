string[] lines = File.ReadAllLines("../input.txt");

char[,] wordSearch = new char[lines.Length, lines[0].Length];

for (int i = 0; i < lines.Length; i++)
{
    for (int j = 0; j < lines[i].Length; j++)
    {
        wordSearch[i, j] = lines[i][j];
    }
}

int xmasFoundCount = 0;

for (int i = 0; i < wordSearch.GetLength(0); i++)
{
    for (int j = 0; j < wordSearch.GetLength(1); j++)
    {
        if (wordSearch[i, j] == 'X')
        {
            // Right
            if (j + 3 < wordSearch.GetLength(1) &&
                wordSearch[i, j] == 'X' &&
                wordSearch[i, j + 1] == 'M' &&
                wordSearch[i, j + 2] == 'A' &&
                wordSearch[i, j + 3] == 'S')
            {
                xmasFoundCount++;
            }

            // Down
            if (i + 3 < wordSearch.GetLength(0) &&
                wordSearch[i, j] == 'X' &&
                wordSearch[i + 1, j] == 'M' &&
                wordSearch[i + 2, j] == 'A' &&
                wordSearch[i + 3, j] == 'S')
            {
                xmasFoundCount++;
            }

            // Left
            if (j - 3 >= 0 &&
                wordSearch[i, j] == 'X' &&
                wordSearch[i, j - 1] == 'M' &&
                wordSearch[i, j - 2] == 'A' &&
                wordSearch[i, j - 3] == 'S')
            {
                xmasFoundCount++;
            }

            // Top
            if (i - 3 >= 0 &&
                wordSearch[i, j] == 'X' &&
                wordSearch[i - 1, j] == 'M' &&
                wordSearch[i - 2, j] == 'A' &&
                wordSearch[i - 3, j] == 'S')
            {
                xmasFoundCount++;
            }

            // Top Right
            if (i - 3 >= 0 && j + 3 < wordSearch.GetLength(1) &&
                wordSearch[i, j] == 'X' &&
                wordSearch[i - 1, j + 1] == 'M' &&
                wordSearch[i - 2, j + 2] == 'A' &&
                wordSearch[i - 3, j + 3] == 'S')
            {
                xmasFoundCount++;
            }

            // Top Left
            if (i - 3 >= 0 && j - 3 >= 0 &&
                wordSearch[i, j] == 'X' &&
                wordSearch[i - 1, j - 1] == 'M' &&
                wordSearch[i - 2, j - 2] == 'A' &&
                wordSearch[i - 3, j - 3] == 'S')
            {
                xmasFoundCount++;
            }

            // Bottom Right
            if (i + 3 < wordSearch.GetLength(0) && j + 3 < wordSearch.GetLength(1) &&
                wordSearch[i, j] == 'X' &&
                wordSearch[i + 1, j + 1] == 'M' &&
                wordSearch[i + 2, j + 2] == 'A' &&
                wordSearch[i + 3, j + 3] == 'S')
            {
                xmasFoundCount++;
            }

            // Bottom Left
            if (i + 3 < wordSearch.GetLength(0) && j - 3 >= 0 &&
                wordSearch[i, j] == 'X' &&
                wordSearch[i + 1, j - 1] == 'M' &&
                wordSearch[i + 2, j - 2] == 'A' &&
                wordSearch[i + 3, j - 3] == 'S')
            {
                xmasFoundCount++;
            }
        } 
    }   
}

Console.WriteLine("Result: " + xmasFoundCount);