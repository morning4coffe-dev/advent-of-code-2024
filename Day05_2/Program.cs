namespace AdventOfCode24;

class Program
{
    static void Main(string[] args)
    {
        var input = Extractor.GetInput();
        var lines = input.Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries);

        Dictionary<int, List<int>> graph = [];
        HashSet<int> allPages = [];

        int index = 0;

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

        List<List<int>> updates = [];

        while (index < lines.Length)
        {
            var update = lines[index].Split(',').Select(int.Parse).ToList();
            updates.Add(update);
            index++;
        }

        int score = 0;

        foreach (var update in updates)
        {
            if (!IsCorrectOrder(update, graph))
            {
                var orderedUpdate = TopologicalSort(update, graph);
                if (orderedUpdate != null)
                {
                    var middleIndex = orderedUpdate.Count / 2;
                    var middlePage = orderedUpdate[middleIndex];
                    score += middlePage;
                }
            }
        }

        Console.WriteLine(score);
    }

    static bool IsCorrectOrder(List<int> update, Dictionary<int, List<int>> graph)
    {
        Dictionary<int, int> pagePositions = [];
        for (int i = 0; i < update.Count; i++)
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

    static List<int> TopologicalSort(List<int> update, Dictionary<int, List<int>> graph)
    {
        var relevantGraph = graph.Where(pair => update.Contains(pair.Key)).ToDictionary(pair => pair.Key, pair => pair.Value.Where(v => update.Contains(v)).ToList());

        Dictionary<int, int> inDegree = [];
        foreach (var page in update)
        {
            if (!inDegree.ContainsKey(page))
            {
                inDegree[page] = 0;
            }
        }

        foreach (var before in relevantGraph.Keys)
        {
            foreach (var after in relevantGraph[before])
            {
                if (inDegree.ContainsKey(after))
                {
                    inDegree[after]++;
                }
            }
        }

        Queue<int> queue = [];
        foreach (var page in inDegree.Where(p => p.Value == 0).Select(p => p.Key))
        {
            queue.Enqueue(page);
        }

        List<int> sortedPages = [];

        while (queue.Count > 0)
        {
            var page = queue.Dequeue();
            sortedPages.Add(page);

            if (relevantGraph.ContainsKey(page))
            {
                foreach (var nextPage in relevantGraph[page])
                {
                    inDegree[nextPage]--;
                    if (inDegree[nextPage] == 0)
                    {
                        queue.Enqueue(nextPage);
                    }
                }
            }
        }

        if (sortedPages.Count != update.Count)
        {
            return null;
        }

        return sortedPages;
    }
}
