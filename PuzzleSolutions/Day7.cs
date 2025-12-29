using System;

namespace AdventOfCode2023.PuzzleSolutions;

public class Day7
{
    private static readonly string ExistingCards = "AKQJT98765432";
    public static string FirstPart(string filePath)
    {
        List<(long strength, long bid)> results = [];
        string input = File.ReadAllText(filePath);
        var parced = input
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(l =>
            {
                var hi = l.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();
                var hand = hi[0];
                var bid = long.Parse(hi[1]);
                long CardStrength = 0;
                var handType = GetHandType(hand);

                long cardOrderMultiplier = (long)Math.Pow(15, hand.Length - 1);
                foreach (var card in hand)
                {
                    CardStrength += CardValue(card) * cardOrderMultiplier;
                    cardOrderMultiplier /= 15;
                }
                return (handType, CardStrength, bid);
            })
            .OrderBy(a => a.handType)
            .ThenBy(a => a.CardStrength)
            .ToArray();

        long totalWinnings = 0;
        int multiplier = 1;
        foreach (var hi in parced)
        {
            totalWinnings += hi.bid * multiplier;
            multiplier++;
        }
        return totalWinnings.ToString();

        static HandType GetHandType(string hand)
        {
            Dictionary<char, long> freq = [];
            foreach (var cha in hand)
            {
                if (freq.TryGetValue(cha, out var v))
                {
                    freq[cha]++;
                }
                else
                {
                    freq.Add(cha, 1);
                }
            }

            HandType handType = HandType.HighCard;
            if (freq.Count(a => a.Value == 2) is 1)
                handType = HandType.OnePair;
            if (freq.Count(a => a.Value == 2) is 2)
                handType = HandType.TwoPair;
            if (freq.Count(a => a.Value == 3) is 1)
                handType = HandType.ThreeOfaKind;
            if (freq.Count(a => a.Value == 3) is 1 && freq.Count(a => a.Value == 2) is 1)
                handType = HandType.FullHouse;
            if (freq.Count(a => a.Value == 4) is 1)
                handType = HandType.FourOfaKind;
            if (freq.Count(a => a.Value == 5) is 1)
                handType = HandType.FiveOfaKind;

            return handType;
        }

        static int CardValue(char card)
        {
            return card switch
            {
                'A' => 14,
                'K' => 13,
                'Q' => 12,
                'J' => 11,
                'T' => 10,
                '9' => 9,
                '8' => 8,
                '7' => 7,
                '6' => 6,
                '5' => 5,
                '4' => 4,
                '3' => 3,
                '2' => 2,
                _ => throw new NotSupportedException()
            };
        }
    }

    public static string SecondPart(string filePath)
    {
        List<(long strength, long bid)> results = [];
        string input = File.ReadAllText(filePath);
        var parced = input
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(line =>
            {
                var lineItems = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();
                var hand = lineItems[0];
                var bid = long.Parse(lineItems[1]);
                long CardStrength = 0;
                var handType = GetBestHandType(hand);

                long cardOrderMultiplier = (long)Math.Pow(15, hand.Length - 1);
                foreach (var card in hand)
                {
                    CardStrength += CardValue(card) * cardOrderMultiplier;
                    cardOrderMultiplier /= 15;
                }
                return (handType, CardStrength, bid);
            })
            .OrderBy(a => a.handType)
            .ThenBy(a => a.CardStrength)
            .ToArray();

        long totalWinnings = 0;
        int multiplier = 1;
        foreach (var (handType, CardStrength, bid) in parced)
        {
            totalWinnings += bid * multiplier;
            multiplier++;
        }
        return totalWinnings.ToString();

        static HandType GetBestHandType(string hand)
        {
            HandType best = HandType.HighCard;

            foreach (var card in ExistingCards)
            {
                var type = GetHandType(hand.Replace('J', card));
                if (type > best)
                    best = type;
            }

            return best;
        }

        static HandType GetHandType(string hand)
        {
            Dictionary<char, long> freq = [];
            foreach (var cha in hand)
            {
                if (freq.TryGetValue(cha, out var v))
                {
                    freq[cha]++;
                }
                else
                {
                    freq.Add(cha, 1);
                }
            }

            if (freq.Count(a => a.Value == 5) is 1)
                return HandType.FiveOfaKind;
            if (freq.Count(a => a.Value == 4) is 1)
                return HandType.FourOfaKind;
            if (freq.Count(a => a.Value == 3) is 1 && freq.Count(a => a.Value == 2) is 1)
               return HandType.FullHouse;
            if (freq.Count(a => a.Value == 3) is 1)
                return HandType.ThreeOfaKind;
            if (freq.Count(a => a.Value == 2) is 2)
                return HandType.TwoPair;
            if (freq.Count(a => a.Value == 2) is 1)
                return HandType.OnePair;

            return HandType.HighCard;
        }

        static int CardValue(char card)
        {
            return card switch
            {
                'A' => 14,
                'K' => 13,
                'Q' => 12,
                'T' => 10,
                '9' => 9,
                '8' => 8,
                '7' => 7,
                '6' => 6,
                '5' => 5,
                '4' => 4,
                '3' => 3,
                '2' => 2,
                'J' => 1,
                _ => throw new NotSupportedException()
            };
        }
    }

    private enum HandType
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfaKind,
        FullHouse,
        FourOfaKind,
        FiveOfaKind,
    }
}
