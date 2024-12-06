namespace AdventOfCode24;

class Program
{
    static void Main(string[] args)
    {
        var input = Extractor.GetInput();
        var lines = input.Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries);

        Dictionary<int, List<int>> graph = [];
        HashSet<int> allPages = [];

        var index = 0;

        while (lines[index].Contains('|'))
        {
            var rule = lines[index].Split('|');
            var before = int.Parse(rule[0]);
            var after = int.Parse(rule[1]);

            if (!graph.ContainsKey(before))
            {
                graph[before] = [];
            }
            graph[before].Add(after);

            allPages.Add(before);
            allPages.Add(after);

            index++;
        }

        var score = 0;

        while (index < lines.Length)
        {
            var update = lines[index].Split(',').Select(int.Parse).ToList();
            index++;

            if (IsCorrectOrder(update, graph))
            {
                var middleIndex = update.Count / 2;
                var middlePage = update[middleIndex];
                score += middlePage;
            }
        }

        Console.WriteLine(score);
    }

    static bool IsCorrectOrder(List<int> update, Dictionary<int, List<int>> graph)
    {
        Dictionary<int, int> pagePositions = [];
        for (var i = 0; i < update.Count; i++)
        {
            pagePositions[update[i]] = i;
        }

        foreach (var rule in graph)
        {
            var before = rule.Key;
            foreach (var after in rule.Value)
            {
                if (pagePositions.ContainsKey(before) && pagePositions.ContainsKey(after))
                {
                    if (pagePositions[before] > pagePositions[after])
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }
}
