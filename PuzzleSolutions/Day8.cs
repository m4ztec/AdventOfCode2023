using System;

namespace AdventOfCode2023.PuzzleSolutions;

public class Day8
{
    public static string FirstPart(string filePath)
    {
        string input = File.ReadAllText(filePath);
        var instructionsAndMap = input.Split($"{Environment.NewLine}{Environment.NewLine}");
        var instruntionsSequence = instructionsAndMap[0];
        var map = instructionsAndMap[1].Split(Environment.NewLine)
            .Select(line =>
            {
                var tmp = line
                    .Split(['=', '(', ',', ')'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                return new Node(tmp[0], tmp[1], tmp[2]);
            })
            .ToDictionary(a => a.Location);

        var currentNode = map["AAA"];
        int stepsTaken = 0;
        while (currentNode.Location != "ZZZ")
        {
            foreach (var step in instruntionsSequence)
            {
                currentNode = step switch
                {
                    'R' => map[currentNode.Right],
                    'L' => map[currentNode.Left],
                    _ => throw new NotSupportedException()
                };

                stepsTaken++;

                if (currentNode.Location == "ZZZ")
                    break;
            }
        }

        return stepsTaken.ToString();
    }

    public static string SecondPart(string filePath)
    {
        string input = File.ReadAllText(filePath);
        var instructionsAndMap = input.Split($"{Environment.NewLine}{Environment.NewLine}");
        var instruntionsSequence = instructionsAndMap[0];
        var map = instructionsAndMap[1].Split(Environment.NewLine)
            .Select(line =>
            {
                var tmp = line
                    .Split(['=', '(', ',', ')'], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                return new Node(tmp[0], tmp[1], tmp[2]);
            })
            .ToDictionary(a => a.Location);

        var currentNodes = map.Where(a => a.Key[2] == 'A').Select(a => a.Value).ToArray();
        int stepsTaken = 0;
        while (currentNodes.All(n => n.Location[2] != 'Z'))
        {
            foreach (var step in instruntionsSequence)
            {
                currentNodes = currentNodes.Select(n => step switch
                    {
                        'R' => map[n.Right],
                        'L' => map[n.Left],
                        _ => throw new NotSupportedException()
                    }).ToArray();

                stepsTaken++;

                if (currentNodes.All(n => n.Location[2] == 'Z'))
                    break;
            }
        }

        return stepsTaken.ToString();
    }

    private record Node(string Location, string Left, string Right);
}
