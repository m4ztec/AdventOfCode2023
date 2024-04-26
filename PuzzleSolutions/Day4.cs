namespace AdventOfCode2023.PuzzleSolutions
{
    public class Day4
    {
        public static string FirstPart(string filePath)
        {
            var result = 0;
            string input = File.ReadAllText(filePath);
            var cards = input.Split(Environment.NewLine);

            foreach (var card in cards)
            {
                var info = card.Split([':', '|']);
                var winningNumbers = info[1].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                var yourNumbers = info[2].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                var matchCount = yourNumbers.Where(a => winningNumbers.Contains(a)).Count();

                if (matchCount > 0)
                {
                    result += (int)Math.Pow(2, matchCount - 1);
                }
            }

            return result.ToString();
        }

        public static string SecondPart(string filePath)
        {
            var result = 0;
            var helpArray = Enumerable.Repeat(1, 11).ToArray();
            string input = File.ReadAllText(filePath);
            var cards = input.Split(Environment.NewLine);

            foreach (var card in cards)
            {
                var info = card.Split([':', '|']);
                var winningNumbers = info[1].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                var yourNumbers = info[2].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                var currentCardAmount = helpArray[0];
                result += currentCardAmount;

                var matchCount = yourNumbers.Where(a => winningNumbers.Contains(a)).Count();

                Array.Copy(helpArray, 1, helpArray, 0, helpArray.Length - 1); // Shift to left
                for (var i = 0; i < matchCount; i++)
                {
                    helpArray[i] += currentCardAmount;
                }
            }

            return result.ToString();
        }
    }
}
