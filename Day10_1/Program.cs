namespace AdventOfCode24;

internal class Program
{
    static void Main(string[] args)
    {
        var input = Extractor.GetInput();
        var rows = input.Length;
        var cols = input[0].Length;
        var map = new int[rows, cols];

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                map[i, j] = input[i][j] - '0';
            }
        }

        int[] dRow = { -1, 1, 0, 0 };
        int[] dCol = { 0, 0, -1, 1 };

        int GetTrailheadScore(int startRow, int startCol)
        {
            var visited = new HashSet<(int, int)>();
            var reachableNines = new HashSet<(int, int)>();
            var queue = new Queue<(int, int)>();
            queue.Enqueue((startRow, startCol));
            visited.Add((startRow, startCol));

            while (queue.Count > 0)
            {
                var (row, col) = queue.Dequeue();
                var currentHeight = map[row, col];

                for (var i = 0; i < 4; i++)
                {
                    var newRow = row + dRow[i];
                    var newCol = col + dCol[i];

                    if (newRow < 0 || newRow >= rows || newCol < 0 || newCol >= cols)
                    {
                        continue;
                    }

                    var newHeight = map[newRow, newCol];

                    if (newHeight == currentHeight + 1 && !visited.Contains((newRow, newCol)))
                    {
                        visited.Add((newRow, newCol));
                        queue.Enqueue((newRow, newCol));

                        if (newHeight == 9)
                        {
                            reachableNines.Add((newRow, newCol));
                        }
                    }
                }
            }

            return reachableNines.Count;
        }

        var totalScore = 0;

        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {
                if (map[row, col] == 0)
                {
                    totalScore += GetTrailheadScore(row, col);
                }
            }
        }

        Console.WriteLine(totalScore);
    }
}