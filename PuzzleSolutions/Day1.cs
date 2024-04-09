namespace AdventOfCode2023.PuzzleSolutions;

public class Day1
{
    public static string FirstPart(string filePath)
    {
        var result = 0;
        string input = File.ReadAllText(filePath);
        var lines = input.Split(Environment.NewLine);

        foreach (var line in lines)
        {
            var first = line.First(char.IsNumber).ToString();
            var last = line.Last(char.IsNumber).ToString();
            result += Convert.ToInt32(first + last);
        }

        return result.ToString();
    }


    public static string SecondPart(string filePath)
    {
        var result = 0;
        string input = File.ReadAllText(filePath);
        var lines = input.Split(Environment.NewLine);

        foreach (var line in lines)
        {
            var first = Search(line);
            var last = SearchBackwards(line);

            // Console.WriteLine($"{line} : {first} : {last}");

            result += Convert.ToInt32(first + last);
        }

        return result.ToString();
    }

    static string Search(string line)
    {
        for (int i = 0; i < line.Length; i++)
        {
            if (char.IsNumber(line[i])) { return line[i].ToString(); }
            if (char.IsNumber(line[i + 1])) { return line[i + 1].ToString(); }

            var search = line.Substring(i, Math.Min(5, line.Length - i));
            if (search == "twone") { return "2"; }
            foreach (var digit in SpelledDigits.Keys)
            {
                if (search.Contains(digit))
                {
                    return SpelledDigits[digit];
                }
            }
        }
        return "";
    }

    static string SearchBackwards(string line)
    {
        for (int i = 1; i < line.Length; i++)
        {
            if (char.IsNumber(line[line.Length - i])) { return line[line.Length - i].ToString(); }
            if (char.IsNumber(line[line.Length - i - 1])) { return line[line.Length - i - 1].ToString(); }
            var hi = Math.Max(line.Length - i - 4, 0);
            var search = line.Substring(Math.Max(line.Length - i - 4, 0), Math.Min(5, line.Length));
            if (search == "twone") { return "1"; }
            foreach (var digit in SpelledDigits.Keys)
            {
                if (search.Contains(digit))
                {
                    return SpelledDigits[digit];
                }
            }
        }
        return "";
    }

    public static readonly Dictionary<string, string> SpelledDigits = new()
    {
        ["zero"] = "0",
        ["one"] = "1",
        ["two"] = "2",
        ["three"] = "3",
        ["four"] = "4",
        ["five"] = "5",
        ["six"] = "6",
        ["seven"] = "7",
        ["eight"] = "8",
        ["nine"] = "9",

    };
}
