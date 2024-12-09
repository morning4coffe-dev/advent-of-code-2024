namespace AdventOfCode24;

class Program
{
    static void Main(string[] args)
    {
        var input = Extractor.GetInput();

        var grid = new char[input.Length][];
        for (int i = 0; i < input.Length; i++)
        {
            grid[i] = input[i].ToCharArray();
        }

        var arrowPosition = new int[2];
        var direction = '^';
        var score = 0;
        var found = false;

        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if ("<^>v".Contains(grid[i][j]))
                {
                    arrowPosition[0] = i;
                    arrowPosition[1] = j;
                    direction = grid[i][j];
                    found = true;
                    break;
                }
            }
            if (found) break;
        }

        var directions = new[] { '<', '^', '>', 'v' };
        var directionVectors = new[]
        {
                new[] { 0, -1 },
                new[] { -1, 0 },
                new[] { 0, 1 },
                new[] { 1, 0 }
            };

        var currentDirectionIndex = Array.IndexOf(directions, direction);

        while (true)
        {
            var nextRow = arrowPosition[0] + directionVectors[currentDirectionIndex][0];
            var nextCol = arrowPosition[1] + directionVectors[currentDirectionIndex][1];

            if (nextRow < 0 || nextRow >= grid.Length || nextCol < 0 || nextCol >= grid[0].Length)
            {
                break;
            }

            if (grid[nextRow][nextCol] == '#')
            {
                currentDirectionIndex = (currentDirectionIndex + 1) % directions.Length;
            }
            else
            {
                if (grid[nextRow][nextCol] == '.')
                {
                    score++;
                    grid[nextRow][nextCol] = 'X';
                }

                arrowPosition[0] = nextRow;
                arrowPosition[1] = nextCol;
            }
        }

        Console.WriteLine($"Final score: {score+1}");

        Console.WriteLine("Updated grid:");
        foreach (var row in grid)
        {
            Console.WriteLine(new string(row));
        }
    }
}