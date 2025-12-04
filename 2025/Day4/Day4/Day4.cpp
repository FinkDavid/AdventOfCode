#include <fstream>
#include <iostream>
#include <string>
#include <vector>

struct Vector2
{
    int x;
    int y;
};


bool CheckForNeighboringRoll(int xPos, int yPos, Vector2 direction, const std::vector<std::vector<char>>& map)
{
    if (xPos + direction.x < 0 || xPos + direction.x >= static_cast<int>(map.size()) ||
        yPos + direction.y < 0 || yPos + direction.y >= static_cast<int>(map[0].size()))
    {
        return false;
    }

    if (map[xPos + direction.x][yPos + direction.y] == '@')
    {
        return true;
    }

    return false;
}

int main(int argc, char* argv[])
{
    std::ifstream file("../input.txt");
    std::vector<std::string> lines;
    std::string line;

    while (std::getline(file, line))
    {
        lines.push_back(line);
    }

    std::vector<Vector2> directions { {0, 1}, {1, 1}, {1, 0}, {1, -1},
                                        {0, -1}, {-1, -1}, {-1, 0}, {-1, 1}};

    int rows = static_cast<int>(lines.size());
    int cols = static_cast<int>(lines[0].size());

    std::vector<std::vector<char>> map(rows, std::vector<char>(cols));

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            map[i][j] = lines[i][j];
        }
    }

    int minNeighbours = 4;
    int result = 0;
    int papersRemoved = 1;

    while (papersRemoved > 0)
    {
        papersRemoved = 0;
        auto tempMap = map;
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i][j] == '@')
                {
                    int neighborCount = 0;
                    for (const auto& dir : directions)
                    {
                        if (CheckForNeighboringRoll(i, j, dir, map))
                        {
                            neighborCount++;
                        }
                    }

                    if (neighborCount < minNeighbours)
                    {
                        tempMap[i][j] = '.';
                        papersRemoved++;
                        result++;
                    }
                }
            }
        }
        
        map = tempMap;
    }
    
    std::cout << "Result: " << result << std::endl;
    
    return 0;
}
