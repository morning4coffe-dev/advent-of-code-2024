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

        var memo = new Dictionary<(int, int), int>();

        int CountPaths(int row, int col)
        {
            if (row < 0 || row >= rows || col < 0 || col >= cols)
            {
                return 0;
            }

            if (map[row, col] == 9)
            {
                return 1;
            }

            if (memo.ContainsKey((row, col)))
            {
                return memo[(row, col)];
            }

            int pathCount = 0;
            var currentHeight = map[row, col];

            for (var i = 0; i < 4; i++)
            {
                var newRow = row + dRow[i];
                var newCol = col + dCol[i];

                if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols &&
                    map[newRow, newCol] == currentHeight + 1)
                {
                    pathCount += CountPaths(newRow, newCol);
                }
            }

            memo[(row, col)] = pathCount;
            return pathCount;
        }

        var totalRating = 0;

        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {
                if (map[row, col] == 0)
                {
                    totalRating += CountPaths(row, col);
                }
            }
        }

        Console.WriteLine(totalRating);
    }
}
