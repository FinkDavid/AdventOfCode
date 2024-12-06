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
        if (wordSearch[i, j] == 'A')
        {
            if (j + 1 < wordSearch.GetLength(1) && j - 1 >= 0 && i + 1 < wordSearch.GetLength(0) && i - 1 >= 0)
            {
                if (CheckForPattern("MSMS", i, j))
                {
                    xmasFoundCount++;
                }

                if (CheckForPattern("MMSS", i, j))
                {
                    xmasFoundCount++;
                }

                if (CheckForPattern("SSMM", i, j))
                {
                    xmasFoundCount++;
                }

                if (CheckForPattern("SMSM", i, j))
                {
                    xmasFoundCount++;
                }
            }
        } 
    }   
}

/*
Pattern it checks for is:

    TOP LEFT     TOP RIGHT
              A
   BOTTOM LEFT  BOTTOM RIGHT
*/
bool CheckForPattern(string pattern, int i, int j)
{
    if (pattern.Length == 4)
    {
        if (wordSearch[i - 1, j - 1] == pattern[0] && wordSearch[i - 1, j + 1] == pattern[1] && 
            wordSearch[i + 1, j - 1] == pattern[2] && wordSearch[i + 1, j + 1] == pattern[3])
        {
            return true;
        }
    }

    return false;
}

Console.WriteLine("Result: " + xmasFoundCount);