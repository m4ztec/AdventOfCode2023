 namespace AdventOfCode2023.PuzzleSolutions;

public class Day3
{
    public static string FirstPart(string filePath)
    {
        var result = 0;
        HashSet<(int, int)> inventory = [];
        string input = File.ReadAllText(filePath);
        var lines = input.Split(Environment.NewLine);

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                var look = lines[i][j];
                if (char.IsNumber(look))
                {
                    search(i, j);
                }
            }
        }

        void search(int i, int j)
        {
            bool hasSymbol = false;
            for (int a = -1; a <= 1; a++)
            {
                if (hasSymbol) { break; }
                for (int b = -1; b <= 1; b++)
                {
                    var tmpY = i + a;
                    var tmpX = j + b;
                    if (tmpX < 0 || tmpX >= lines[0].Length || tmpY < 0 || tmpY >= lines.Length)
                    {
                        continue;
                    }
                    if (!char.IsNumber(lines[tmpY][tmpX]) && lines[tmpY][tmpX] != '.')
                    {
                        hasSymbol = true;
                    }
                    if (hasSymbol)
                    {
                        (int, int) first = (i, j);
                        var offset = 1;
                        while ((j - offset) >= 0 && char.IsNumber(lines[i][j - offset]))
                        {
                            first = (i, j - offset);
                            offset++;
                        }
                        inventory.Add(first);
                    }
                }
            }
        }


        foreach (var item in inventory)
        {
            var (i, j) = item;
            List<char> num = [lines[i][j]];
            var offset = 1;
            while ((j + offset) < lines[0].Length && char.IsNumber(lines[i][j + offset]))
            {
                num.Add(lines[i][j + offset]);
                offset++;
            }
            result += Convert.ToInt32(new string(num.ToArray()));
        }

        return result.ToString();
    }

    public static string SecondPart(string filePath)
    {
        var result = 0;
        List<HashSet<(int, int)>> inventory = [];
        string input = File.ReadAllText(filePath);
        var lines = input.Split(Environment.NewLine);

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[i].Length; j++)
            {
                var look = lines[i][j];
                if (look == '*')
                {
                    search(i, j);
                }
            }
        }

        void search(int i, int j)
        {
            HashSet<(int, int)> touchingStarSymbol = [];
            for (int a = -1; a <= 1; a++)
            {
                for (int b = -1; b <= 1; b++)
                {
                    var tmpY = i + a;
                    var tmpX = j + b;
                    if (tmpX < 0 || tmpX >= lines[0].Length || tmpY < 0 || tmpY >= lines.Length)
                    {
                        continue;
                    }
                    if (!char.IsNumber(lines[tmpY][tmpX]))
                    {
                        continue;
                    }

                    (int, int) first = (tmpY, tmpX);
                    var offset = 1;
                    while ((tmpX - offset) >= 0 && char.IsNumber(lines[tmpY][tmpX - offset]))
                    {
                        first = (tmpY, tmpX - offset);
                        offset++;
                    }
                    touchingStarSymbol.Add(first);

                }
            }
            if (touchingStarSymbol.Count == 2)
            {
                inventory.Add(touchingStarSymbol);
            }
        }


        foreach (var item in inventory)
        {
            List<int> gearNumbers = [];
            foreach (var item2 in item)
            {
                var (i, j) = item2;
                List<char> num = [lines[i][j]];
                var offset = 1;
                while ((j + offset) < lines[0].Length && char.IsNumber(lines[i][j + offset]))
                {
                    num.Add(lines[i][j + offset]);
                    offset++;
                }
                gearNumbers.Add(Convert.ToInt32(new string(num.ToArray())));
            }
            result += gearNumbers[0] * gearNumbers[1];
        }

        return result.ToString();
    }
}
