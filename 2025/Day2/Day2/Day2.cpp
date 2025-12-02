#include <fstream>
#include <iostream>
#include <list>
#include <regex>
#include <string>

struct Vector2ll {
    long long x;
    long long y;
};

std::pair<bool,std::string> HasRepeatingPattern(const std::string &s)
{
    int n = static_cast<int>(s.size());
    if (n <= 1) return {false, ""};

    for (int k = 1; k <= n/2; ++k)
    {
        if (n % k != 0) continue;
        std::string unit = s.substr(0, k);
        bool ok = true;

        for (int i = 0; i < n; i += k)
        {
            if (s.compare(i, k, unit) != 0)
            {
                ok = false;
                break;
            }
        }

        if (ok)
        {
            return {true, unit};
        }
    }

    return {false, ""};
}

int main(int argc, char* argv[])
{
    std::ifstream file("../input.txt");

    std::string line;
    std::list<Vector2ll> numberSets;

    std::regex pairRgx(R"((\d*)-(\d*))");

    while (std::getline(file, line))
    {
        for (std::sregex_iterator it(line.begin(), line.end(), pairRgx), end; it != end; ++it)
        {
            std::smatch m = *it;
            long long x = std::stoll(m[1].str());
            long long y = std::stoll(m[2].str());
            numberSets.push_back(Vector2ll{ x, y });
        }
    }

    long long result = 0;
    
    for (const auto& numberSet : numberSets)
    {
        for (long long currentNumber = numberSet.x; currentNumber <= numberSet.y; currentNumber++)
        {
            std::string s = std::to_string(currentNumber);
            auto RepeatingPatternResult = HasRepeatingPattern(s);
            
            if (RepeatingPatternResult.first)
            {
                std::cout << currentNumber << " -> repeating unit: '" << RepeatingPatternResult.second <<
                    " number range: " << numberSet.x << " - " << numberSet.y << "'\n";
                result += currentNumber;
            }
        }
    }

    std::cout << "Result: " << result << "\n";

    return 0;
}
