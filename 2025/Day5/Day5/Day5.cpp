#include <algorithm>
#include <fstream>
#include <iostream>
#include <regex>
#include <string>
#include <vector>

struct Vector2
{
    long long x;
    long long y;
};

int main(int argc, char* argv[])
{
    std::ifstream file("../input.txt");
    std::vector<std::string> lines;
    std::string line;

    while (std::getline(file, line))
    {
        lines.push_back(line);
    }

    bool readRanges = true;

    std::vector<Vector2> ranges;
    std::vector<long long> availableIngredientIDs;

    int length = static_cast<int>(lines.size());

    for (int i = 0; i < length; i++)
    {
        if (lines[i].empty())
        {
            readRanges = false;
            continue;
        }

        if (readRanges)
        {
            std::regex regex(R"((\d+)-(\d+))");
            std::smatch match;

            if (std::regex_match(lines[i], match, regex))
            {
                long long a = stoll(match[1].str());
                long long b = stoll(match[2].str());
                ranges.push_back({a, b});
            }
        }
        else
        {
            availableIngredientIDs.push_back(stoll(lines[i]));
        }
    }

    // Sort by start
    std::sort(ranges.begin(), ranges.end(), [](const Vector2& r1, const Vector2& r2)
    {
        if (r1.x != r2.x)
        {
            return r1.x < r2.x;
        }
        
        return r1.y < r2.y;
    });

    // Merge intervals
    std::vector<Vector2> merged;
    merged.push_back(ranges[0]);

    for (int i = 1; i < static_cast<int>(ranges.size()); i++) 
    {
        Vector2 &last = merged.back();
        const Vector2 &currentRange = ranges[i];

        if (currentRange.x <= last.y + 1)
        {
            last.y = std::max(currentRange.y, last.y);
        }
        else
        {
            merged.push_back(currentRange);
        }
    }

    long long result = 0;
    
    for (const auto &r : merged)
    {
        result += r.y - r.x + 1;
    }
    
    std::cout << "Result: " << result << '\n';
    
    return 0;
}
