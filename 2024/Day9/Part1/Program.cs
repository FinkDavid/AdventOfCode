using System.Data.Common;

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

int startLookingAt = 0;

for (int i = blocks.Count - 1; i >= 0; i--)
{
    if (blocks[i] == -1)
    {
        continue;
    }

    int tempID = blocks[i];
    
    for (int j = startLookingAt; j < updatedBlocks.Count; j++)
    {
        if (j <= i)
        {
            if (updatedBlocks[j] == -1)
            {
                updatedBlocks[j] = tempID;

                updatedBlocks[i] = -1;

                startLookingAt = j + 1;
                break;
            }
        }
    }
}

for (int i = updatedBlocks.Count - 1; i >= 0; i--)
{
    if (updatedBlocks[i] == -1)
    {
        updatedBlocks.RemoveAt(i);
    }
}

long result = 0;

for (int i = 0; i < updatedBlocks.Count; i++)
{
    result += i * int.Parse(updatedBlocks[i].ToString());
}

Console.WriteLine("Result: " + result);