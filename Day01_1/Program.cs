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

        var sortedLeft = Sort(leftList);
        var sortedRight = Sort(rightList);

        var distance = 0;

        for (var i = 0; i < sortedLeft.Count; i++)
        {
            distance += Math.Abs(sortedLeft[i] - sortedRight[i]);
        }

        Console.WriteLine(distance);
    }

    static List<int> Sort(List<int> list)
    {
        for (var i = 0; i < list.Count - 1; i++)
        {
            for (var j = 0; j < list.Count - i - 1; j++)
            {
                if (list[j] > list[j + 1])
                {
                    var temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                }
            }
        }

        return list;
    }
}
