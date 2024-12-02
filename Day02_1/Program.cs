using System.Runtime.CompilerServices;

namespace AdventOfCode24;

internal class Program
{
    static void Main(string[] args)
    {
        var score = 0;

        foreach (var line in Extractor.GetInput())
        {
            var splitLine = line.Split(" ");

            bool? isIncreasing = null;
            var isSafe = true;

            for (var i = 0; i < splitLine.Length; i++)
            {
                if (i + 1 >= splitLine.Length)
                {
                    break;
                }

                var currentLevel = int.Parse(splitLine[i]);
                var nextLevel = int.Parse(splitLine[i + 1]);

                var difference = nextLevel - currentLevel;

                if (isIncreasing == null)
                {
                    isIncreasing = difference > 0;
                }
                else
                if (isIncreasing != (difference > 0))
                {
                    isSafe = false;
                    break;
                }

                if (Math.Abs(difference) < 1 || Math.Abs(difference) > 3)
                {
                    isSafe = false;
                    break;
                }
            }

            if (isSafe)
            {
                score++;
            }
        }

        Console.WriteLine(score);
    }
}
