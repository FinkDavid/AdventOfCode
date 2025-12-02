#include <fstream>
#include <iosfwd>
#include <iostream>
#include <string>
#include <vector>

int main(int argc, char* argv[])
{
    std::ifstream file("../input.txt");
    std::vector<std::string> lines;
    std::string line;

    while (std::getline(file, line))
    {
        lines.push_back(line);
    }

    int currentNumber = 50;
    int result = 0;

    for (auto& l : lines)
    {
        std::string oldLine = l;
        char direction = l[0];
        l.erase(l.begin());
        int number = std::stoi(l);

        if (direction == 'R')
        {
            // first step to reach 0:
            int k_first = (100 - currentNumber) % 100;
            if (k_first == 0) k_first = 100;
            if (number >= k_first) {
                result += 1 + (number - k_first) / 100;
            }
            currentNumber = (currentNumber + number) % 100;
        }
        else if (direction == 'L')
        {
            int k_first = currentNumber % 100;
            if (k_first == 0) k_first = 100;
            if (number >= k_first) {
                result += 1 + (number - k_first) / 100;
            }
            
            int tmp = (currentNumber - number) % 100;
            if (tmp < 0) tmp += 100;
            currentNumber = tmp;
        }
    }

    std::cout << "Result: " << result << "\n";

    return 0;
}
