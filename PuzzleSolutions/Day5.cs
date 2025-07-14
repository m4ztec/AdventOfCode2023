

namespace AdventOfCode2023.PuzzleSolutions;

public class Day5
{

    public static string FirstPart(string filePath)
    {
        List<long> results = [];
        string input = File.ReadAllText(filePath);
        var lines = input.Split($"{Environment.NewLine}{Environment.NewLine}").Where(a => a.Length > 0);
        var seeds = lines.First().Split(":").Last().Trim().Split(" ").Select(long.Parse);
        var domainConvertions = lines.Skip(1);

        foreach (long seed in seeds)
        {
            long tracked = seed;
            foreach (var domainConvertion in domainConvertions)
            {
                string[] maps = [.. domainConvertion.Split(":").Last().Trim().Split(Environment.NewLine)];
                foreach (var map in maps)
                {   
                    long[] mapInfo = [.. map.Trim().Split(" ").Select(long.Parse)];
                    (long destinationRangeStart, long sourceRangeStart, long RangeSize) = (mapInfo[0], mapInfo[1], mapInfo[2]);
                    var isConverted = Convert_to(tracked, destinationRangeStart, sourceRangeStart, RangeSize, out tracked);
                    if (isConverted)
                    {
                        break;
                    }
                }
            }
            results.Add(tracked);
        }

        return results.Min().ToString();
    }

    public static string SecondPart(string filePath)
    {
        List<long> results = [];
        string input = File.ReadAllText(filePath);
        var lines = input.Split($"{Environment.NewLine}{Environment.NewLine}").Where(a => a.Length > 0);






        
        return "";
    }

    public static bool Convert_to(long input, long destinationRangeStart, long sourceRangeStart, long RangeSize, out long output)
    {
        output = input;
        if (input < sourceRangeStart || input > sourceRangeStart + RangeSize)
        {
            return false;
        }

        var diff = Math.Abs(input - sourceRangeStart);
        output = destinationRangeStart + diff;

        return true;
    }

}
