#include <algorithm>
#include <fstream>
#include <iostream>
#include <map>
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

    long long result = 0;

    for (auto& l : lines)
    {
        int batteryCount = 2;
        int lineLength = static_cast<int>(l.length()) - 1;
        int digitsNeeded = batteryCount;

        int lastDigitFoundIndex = -1;

        std::string finalNumber;
        while (static_cast<int>(finalNumber.length()) < batteryCount)
        {
            int highestDigit = -1;
            int highestDigitIndex = -1;
            
            for (int i = lineLength - digitsNeeded + 1; i > lastDigitFoundIndex; i--)
            {
                int currentDigit = l[i] - '0';
                
                if (currentDigit >= highestDigit)
                {
                    highestDigit = currentDigit;
                    highestDigitIndex = i;
                }
            }

            finalNumber += l[highestDigitIndex];
            digitsNeeded--;
            lastDigitFoundIndex = highestDigitIndex;
        }

        result += std::stoll(finalNumber);
    }

    std::cout << "Result: " << result << '\n';
    
    return 0;
}
