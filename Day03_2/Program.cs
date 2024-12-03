using System.Text.RegularExpressions;

namespace AdventOfCode24;

internal partial class Program
{
    static void Main(string[] args)
    {
        var score = 0;

        var instructions = FunctionsRegex().Matches(Extractor.GetInput());
        var isDo = true;

        foreach (Match instruction in instructions)
        {
            if (instruction.Value == "do()" || instruction.Value == "don't()")
            {
                isDo = instruction.Value == "do()";
                continue;
            }

            if (isDo)
            {
                var values = instruction.Value.Split(',');
                var a = int.Parse(values[0].Substring(4));
                var b = int.Parse(values[1].Substring(0, values[1].Length - 1));
                score += a * b;
            }
        }

        Console.WriteLine(score);
    }

    [GeneratedRegex("(do\\(\\)|don't\\(\\)|mul\\(\\d+,\\s*\\d+\\))")]
    internal static partial Regex FunctionsRegex();
}
