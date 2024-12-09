namespace AdventOfCode24;

public class Solution
{
    static void Main(string[] args)
    {
        var input = Extractor.GetInput();

        var grid = ParseGrid(input);
        var (startPos, startDirection) = FindInitialArrowPosition(grid);
        var loopPositions = new HashSet<(int, int)>();

        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] != '.')
                {
                    continue;
                }

                var gridCopy = DeepCopyGrid(grid);
                gridCopy[i][j] = '#';

                if (CausesInfiniteLoop(gridCopy, startPos, startDirection))
                {
                    loopPositions.Add((i, j));
                }
            }
        }

        Console.WriteLine($"Number of positions that cause a loop: {loopPositions.Count}");
    }

    private static (int[], char) FindInitialArrowPosition(char[][] grid)
    {
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if ("<^>v".Contains(grid[i][j]))
                {
                    return (new[] { i, j }, grid[i][j]);
                }
            }
        }
        throw new InvalidOperationException("No arrow found in grid");
    }

    private static bool CausesInfiniteLoop(char[][] grid, int[] startPos, char startDirection)
    {
        var pos = (row: startPos[0], col: startPos[1]);
        var currentDirection = startDirection;
        var directions = new[] { '<', '^', '>', 'v' };
        var directionVectors = new Dictionary<char, (int dx, int dy)>
        {
            {'<', (0, -1)}, {'^', (-1, 0)}, {'>', (0, 1)}, {'v', (1, 0)}
        };

        var visited = new HashSet<(int row, int col, char dir)>();
        int steps = 0;
        int maxSteps = grid.Length * grid[0].Length * 4;

        while (steps < maxSteps)
        {
            var currentState = (pos.row, pos.col, currentDirection);
            if (visited.Contains(currentState))
            {
                return true;
            }
            visited.Add(currentState);

            var (dx, dy) = directionVectors[currentDirection];
            var nextRow = pos.row + dx;
            var nextCol = pos.col + dy;

            if (nextRow < 0 || nextRow >= grid.Length ||
                nextCol < 0 || nextCol >= grid[0].Length)
            {
                return false;
            }

            if (grid[nextRow][nextCol] == '#')
            {
                int currentIndex = Array.IndexOf(directions, currentDirection);
                currentDirection = directions[(currentIndex + 1) % 4];
            }
            else
            {
                pos = (nextRow, nextCol);
            }

            steps++;
        }

        return false;
    }

    private static char[][] ParseGrid(string[] input)
    {
        var grid = new char[input.Length][];
        for (int i = 0; i < input.Length; i++)
        {
            grid[i] = input[i].ToCharArray();
        }
        return grid;
    }

    private static char[][] DeepCopyGrid(char[][] original)
    {
        var copy = new char[original.Length][];
        for (int i = 0; i < original.Length; i++)
        {
            copy[i] = new char[original[i].Length];
            Array.Copy(original[i], copy[i], original[i].Length);
        }
        return copy;
    }
}