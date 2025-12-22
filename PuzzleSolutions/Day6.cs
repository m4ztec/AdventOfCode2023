namespace AdventOfCode2023.PuzzleSolutions;

public class Day6
{
    public static string FirstPart(string filePath)
    {
        List<long> results = [];
        string input = File.ReadAllText(filePath);
        var lines = input
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(l =>
                l.Split(":")
                .Last()
                .Split("  ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray()
            )
            .ToArray();

        var races = lines[0].Zip(lines[1]);

        foreach ((var time, var distance) in races)
        {
            long result = 0;
            for (long hold = 0; hold <= time; hold++)
            {
                var dis = -hold * (hold - time);
                if (dis > distance)
                    result++;
            }

            results.Add(result);
        }

        return results.Aggregate(1L, (acc, x) => acc * x).ToString();
    }

    public static string SecondPart(string filePath)
    {
        List<long> results = [];
        string input = File.ReadAllText(filePath);
        var parsed = input.Replace(" ", "")
            .Split(Environment.NewLine)
            .Select(l =>
               long.Parse(l.Split(":").Last())
            ).ToArray();
        (var time, var distance) = (parsed[0], parsed[1]);

        (var min, var max) = SolveQuadratic(-1, time, -distance);

        var maxRounded = (long)Math.Round(max, 0, MidpointRounding.ToZero);
        var minRounded = (long)Math.Round(min, 0, MidpointRounding.AwayFromZero);

        return Math.Abs(maxRounded - minRounded).ToString();

        static (double x1, double x2) SolveQuadratic(long a, long b, long c)
        {
            double d = b * b - 4 * a * c;
            double sqrt = Math.Sqrt(d);
            var x1 = (-b + sqrt) / (2 * a);
            var x2 = (-b - sqrt) / (2 * a);
            return (Math.Min(x1, x2), Math.Max(x1, x2));
        }
    }
}
