string[] lines = File.ReadAllLines("../input.txt");

//===============================================================
// PARSING
//===============================================================

List<int> blocks = new List<int>();
int currentID = 0;

foreach (string line in lines)
{
    for (int i = 0; i < line.Length; i++)
    {
        if (i % 2 == 0)
        {
            int amount = int.Parse(line[i].ToString());

            for (int j = 0; j < amount; j++)
            {
                blocks.Add(currentID);
            }

            currentID++;
        }
        else
        {
            int amount = int.Parse(line[i].ToString());

            for (int j = 0; j < amount; j++)
            {
                blocks.Add(-1);
            }
        }
    }
}

//===============================================================
// LOGIC
//===============================================================

List<int> updatedBlocks = new List<int>(blocks);

for (int i = blocks.Count - 1; i >= 0; i--)
{
    if (blocks[i] == -1)
    {
        continue;
    }

    int currentFileID = blocks[i];
    int currentFileSize = 0;

    for (int j = i; j >= 0; j--)
    {
        if (blocks[j] == currentFileID)
        {
            currentFileSize++;
        }
        else
        {
            break;
        }
    }

    for (int j = 0; j < updatedBlocks.Count; j++)
    {
        if (j <= i)
        {
            if (updatedBlocks[j] == -1)
            {
                int currentEmptySize = 0;

                for (int k = j; k < updatedBlocks.Count; k++)
                {
                    if (updatedBlocks[k] == -1)
                    {
                        currentEmptySize++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (currentEmptySize >= currentFileSize)
                {
                    for (int x = 0; x < currentFileSize; x++)
                    {
                        updatedBlocks[j + x] = currentFileID;
                        updatedBlocks[i - x] = -1;
                    }

                    break;
                }
                else
                {
                    j += currentEmptySize;
                }
            }
        }
    }

    i -= currentFileSize - 1;
}

long result = 0;

for (int i = 0; i < updatedBlocks.Count; i++)
{
    if (updatedBlocks[i] != -1)
    {
        result += i * int.Parse(updatedBlocks[i].ToString());
    }
}

Console.WriteLine("Result: " + result);