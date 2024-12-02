namespace AdventOfCode24;

internal class Program
{
    static void Main(string[] args)
    {
        var score = 0;

        foreach (var line in Extractor.GetInput())
        {
            var levels = line.Split(" ").Select(int.Parse).ToArray();

            if (IsSafe(levels))
            {
                score++;
                continue;
            }

            for (int i = 0; i < levels.Length; i++)
            {
                var modifiedLevels = levels.Where((_, index) => index != i).ToArray();
                if (IsSafe(modifiedLevels))
                {
                    score++;
                    break;
                }
            }
        }

        Console.WriteLine(score);
    }

    static bool IsSafe(int[] levels)
    {
        bool? isIncreasing = null;

        for (int i = 0; i < levels.Length - 1; i++)
        {
            int currentLevel = levels[i];
            int nextLevel = levels[i + 1];
            int difference = nextLevel - currentLevel;

            if (isIncreasing == null)
            {
                isIncreasing = difference > 0;
            }
            else if (isIncreasing != (difference > 0))
            {
                return false;
            }

            if (Math.Abs(difference) < 1 || Math.Abs(difference) > 3)
            {
                return false;
            }
        }

        return true;
    }
}
