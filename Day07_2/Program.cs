namespace AdventOfCode24;

internal class Program
{
    static void Main(string[] args)
    {
        long calibrationResult = 0;

        foreach (var line in Extractor.GetInput())
        {
            var splitLine = line.Split(":");
            var operationResult = long.Parse(splitLine[0]);
           
            var stringNumbers = splitLine[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var numbers = stringNumbers.Select(long.Parse).ToArray();
           
            if (TryAllCombinations(numbers, operationResult, 1, numbers[0]))
            {
                calibrationResult += operationResult;
            }
        }

        Console.WriteLine(calibrationResult);
    }

    static bool TryAllCombinations(long[] numbers, long target, int index, long currentValue)
    {
        if (index == numbers.Length)
        {
            return currentValue == target;
        }

        if (currentValue > target && !numbers.Skip(index).Any(n => n == 1))
        {
            return false;
        }

        if (TryAllCombinations(numbers, target, index + 1, currentValue + numbers[index]))
        {
            return true;
        }

        if (TryAllCombinations(numbers, target, index + 1, currentValue * numbers[index]))
        {
            return true;
        }

        long concatenatedValue = long.Parse(currentValue.ToString() + numbers[index].ToString());
        if (concatenatedValue <= target && TryAllCombinations(numbers, target, index + 1, concatenatedValue))
        {
            return true;
        }

        return false;
    }
}