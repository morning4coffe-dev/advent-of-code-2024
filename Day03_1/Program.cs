using System.Text.RegularExpressions;

namespace AdventOfCode24;

internal partial class Program
{
    static void Main(string[] args)
    {
        var score = 0;

        var mulInstructions = MulRegex().Matches(Extractor.GetInput());

        foreach (Match mulInstruction in mulInstructions)
        {
            var values = mulInstruction.Value.Split(',');
            var a = int.Parse(values[0].Substring(4));
            var b = int.Parse(values[1].Substring(0, values[1].Length - 1));
            score += a * b;
        }

        Console.WriteLine(score);
    }

    [GeneratedRegex("mul\\(\\d+,\\d+\\)")]
    internal static partial Regex MulRegex();
}
