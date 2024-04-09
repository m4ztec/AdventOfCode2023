namespace AdventOfCode2023.PuzzleSolutions;

public class Day2
{
    public static string FirstPart(string filePath)
    {
        var result = 0;
        string input = File.ReadAllText(filePath);
        var matches = input.Split(Environment.NewLine);

        foreach (var match in matches)
        {
            bool shouldCount = false;
            var tmp = match.Split(":");
            var gameId = int.Parse(tmp[0].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)[1]);
            var shows = tmp[1].Split([';', ',']);

            foreach (var show in shows)
            {
                var info = show.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                var cubesNumer = int.Parse(info[0]);
                var cubesColer = info[1];
                if (cubesNumer > CubeLimits[cubesColer])
                {
                    shouldCount = false;
                    break;
                }
                shouldCount = true;
            }
            if (shouldCount)
            {
                result += gameId;
            }
        }

        return result.ToString();
    }

    public static string SecondPart(string filePath)
    {
        var result = 0;
        string input = File.ReadAllText(filePath);
        var matches = input.Split(Environment.NewLine);

        foreach (var match in matches)
        {
            Dictionary<string, int> inventory = new()
            {
                ["red"] = 0,
                ["green"] = 0,
                ["blue"] = 0,
            };
            bool shouldCount = false;
            var tmp = match.Split(":");
            var gameId = int.Parse(tmp[0].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)[1]);
            var shows = tmp[1].Split([';', ',']);

            foreach (var show in shows)
            {
                var info = show.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                var cubesNumer = int.Parse(info[0]);
                var cubesColer = info[1];

                if (cubesNumer > inventory[cubesColer])
                {
                    inventory[cubesColer] = cubesNumer;
                }
            }

            result += inventory.Values.Aggregate((a, x) => a * x);
        }

        return result.ToString();
    }


    public static readonly Dictionary<string, int> CubeLimits = new()
    {
        ["red"] = 12,
        ["green"] = 13,
        ["blue"] = 14,
    };
}

