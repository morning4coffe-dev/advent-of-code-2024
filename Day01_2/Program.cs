namespace AdventOfCode24;

internal class Program
{
    static void Main(string[] args)
    {
        var leftList = new List<int>();
        var rightList = new List<int>();

        foreach (var line in Extractor.GetInput())
        {
            var splitLine = line.Split("   ");
            leftList.Add(int.Parse(splitLine[0]));
            rightList.Add(int.Parse(splitLine[1]));
        }

        var score = 0;

        foreach (var left in leftList)
        {
            var localScore = 0;
            foreach (var right in rightList)
            {
                if (left == right)
                {
                    localScore++;
                }
            }
            score += left * localScore;
        }

        Console.WriteLine(score);
    }
}
