var inputs = File.ReadAllLines("./input.txt");

var fiveOAKind = new List<(string, int)>();
var fourOAKind = new List<(string, int)>();
var fullHouses = new List<(string, int)>();
var threeOAKind = new List<(string, int)>();
var twoPair = new List<(string, int)>();
var onePair = new List<(string, int)>();
var highCard = new List<(string, int)>();

foreach (var input in inputs)
{
    var splitInput = input.Split(" ");
    var distinctValues = splitInput[0].Distinct();
    if (distinctValues.Count() == 1)
    {
        fiveOAKind.Add((splitInput[0], int.Parse(splitInput[1])));
        continue;
    }

    if (distinctValues.Count() == 2)
    {
        var foundHand = false;
        foreach (var distinctValue in distinctValues)
        {
            if (splitInput[0].Count(x => x == distinctValue || x == 'J') == 5)
            {
                fiveOAKind.Add((splitInput[0], int.Parse(splitInput[1])));
                foundHand = true;
                break;
            }
        }

        if (foundHand)
        {
            continue;
        }


        foreach (var distinctValue in distinctValues)
        {
            if (splitInput[0].Count(x => x == distinctValue || x == 'J') == 4)
            {
                fourOAKind.Add((splitInput[0], int.Parse(splitInput[1])));
                foundHand = true;
                break;
            }

        }
        if (foundHand)
        {
            continue;
        }

        foreach (var distinctValue in distinctValues)
        {
            if (splitInput[0].Count(x => x == distinctValue) == 3)
            {
                fullHouses.Add((splitInput[0], int.Parse(splitInput[1])));
                foundHand = true;
                break;
            }
        }
        if (foundHand)
        {
            continue;
        }
    }

    if (distinctValues.Count() == 3 && !distinctValues.Contains('J'))
    {
        var foundHand = false;
        foreach (var distinctValue in distinctValues)
        {
            if (splitInput[0].Count(x => x == distinctValue) == 3)
            {
                threeOAKind.Add((splitInput[0], int.Parse(splitInput[1])));
                foundHand = true;
                break;
            }

            if (splitInput[0].Count(x => x == distinctValue) == 2)
            {
                twoPair.Add((splitInput[0], int.Parse(splitInput[1])));
                foundHand = true;
                break;
            }
        }
        if (foundHand)
        {
            continue;
        }
    }

    if (distinctValues.Count() == 3)
    {
        var foundHand = false;
        foreach (var distinctValue in distinctValues)
        {
            if (splitInput[0].Count(x => x == distinctValue || x == 'J') == 4)
            {
                fourOAKind.Add((splitInput[0], int.Parse(splitInput[1])));
                foundHand = true;
                break;
            }
        }
        if (foundHand)
        {
            continue;
        }


        foreach (var distinctValue in distinctValues)
        {
            if (splitInput[0].Count(x => x == distinctValue || x == 'J') == 3)
            {
                fullHouses.Add((splitInput[0], int.Parse(splitInput[1])));
                foundHand = true;
                break;
            }
        }
        if (foundHand)
        {
            continue;
        }

        foreach (var distinctValue in distinctValues)
        {
            if (splitInput[0].Count(x => x == distinctValue || x == 'J') == 3)
            {
                threeOAKind.Add((splitInput[0], int.Parse(splitInput[1])));
                foundHand = true;
                break;
            }
        }
        if (foundHand)
        {
            continue;
        }

        foreach (var distinctValue in distinctValues)
        {
            if (splitInput[0].Count(x => x == distinctValue) == 2)
            {
                twoPair.Add((splitInput[0], int.Parse(splitInput[1])));
                foundHand = true;
                break;
            }
        }
        if (foundHand)
        {
            continue;
        }
    }

    if (distinctValues.Count() == 4 && distinctValues.Contains('J'))
    {
        var foundHand = false;
        foreach (var distinctValue in distinctValues)
        {
            if (splitInput[0].Count(x => x == distinctValue || x == 'J') == 3)
            {
                threeOAKind.Add((splitInput[0], int.Parse(splitInput[1])));
                foundHand = true;
                break;
            }
        }
        if (foundHand)
        {
            continue;
        }
    }

    if (splitInput[0].Count(x => x == 'J') == 2)
    {
        twoPair.Add((splitInput[0], int.Parse(splitInput[1])));
        continue;
    }

    if (splitInput[0].Count(x => x == 'J') == 1)
    {
        onePair.Add((splitInput[0], int.Parse(splitInput[1])));
        continue;
    }

    if (distinctValues.Count() == 5)
    {
        highCard.Add((splitInput[0], int.Parse(splitInput[1])));
        continue;
    }
    onePair.Add((splitInput[0], int.Parse(splitInput[1])));
}

int count = inputs.Length;
(long, int) result = new(0, count);
result = GetBidTotal(fiveOAKind, result);
result = GetBidTotal(fourOAKind, result);
result = GetBidTotal(fullHouses, result);
result = GetBidTotal(threeOAKind, result);
result = GetBidTotal(twoPair, result);
result = GetBidTotal(onePair, result);
result = GetBidTotal(highCard, result);

Console.WriteLine(result.Item1);

(long, int) GetBidTotal(List<(string, int)> hands, (long, int) result)
{
    foreach (var hand in hands
                 .OrderByDescending(x => GetValue(x.Item1[0]))
                 .ThenByDescending(x => GetValue(x.Item1[1]))
                 .ThenByDescending(x => GetValue(x.Item1[2]))
                 .ThenByDescending(x => GetValue(x.Item1[3]))
                 .ThenByDescending(x => GetValue(x.Item1[4])))
    {
        result.Item1 += hand.Item2 * result.Item2;
        result.Item2--;
    }
    return (result.Item1, result.Item2);
}

int GetValue(char charValue) => charValue switch
{
    'T' => 10,
    'J' => 1,
    'Q' => 12,
    'K' => 13,
    'A' => 14,
    _ => int.Parse(charValue.ToString())
};