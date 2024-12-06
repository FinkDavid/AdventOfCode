string[] lines = File.ReadAllLines("../input.txt");

List<(int, int)> rules = new List<(int, int)>();
List<List<int>> pageUpdates = new List<List<int>>();

bool isSecondSection = false;
int updateIndex = 0;

foreach (string line in lines)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        isSecondSection = true;
        continue;
    }

    if (!isSecondSection)
    {
        string[] parts = line.Split('|');
        rules.Add((int.Parse(parts[0]), int.Parse(parts[1])));
    }
    else
    {
        string[] parts = line.Split(',');

        pageUpdates.Add(new List<int>());
        foreach (string part in parts)
        {
            pageUpdates[updateIndex].Add(int.Parse(part));
        }

        updateIndex++;
    }
}

int result = 0;

for (int i = 0; i < pageUpdates.Count; i++)
{
    bool isInCorrectOrder = true;
    foreach (int pageUpdate in pageUpdates[i])
    {
        foreach ((int, int) rule in rules)
        {
            if (pageUpdate == rule.Item1)
            {
                // Check if the 2nd page is after the updated page
                int indexOfUpdatedPage = pageUpdates[i].IndexOf(pageUpdate);
                int indexOf2ndPage = pageUpdates[i].IndexOf(rule.Item2);

                if (indexOf2ndPage == -1)
                {
                    continue;
                }
                else if (indexOf2ndPage < indexOfUpdatedPage)
                {
                    isInCorrectOrder = false;
                    break;
                }
            }
            else if(pageUpdate == rule.Item2)
            {
                // Check if the 1st page is before the updated page
                int indexOfUpdatedPage = pageUpdates[i].IndexOf(pageUpdate);
                int indexOf1stPage = pageUpdates[i].IndexOf(rule.Item1);

                if (indexOf1stPage == -1)
                {
                    continue;
                }
                else if (indexOf1stPage > indexOfUpdatedPage)
                {
                    isInCorrectOrder = false;
                    break;
                }
            }
            else
            {
                continue;
            }
        }

        if (!isInCorrectOrder)
        {
            break;
        }
    }

    if (isInCorrectOrder)
    {
        result += pageUpdates[i][pageUpdates[i].Count / 2];
    }
}

Console.WriteLine("Result: " + result);