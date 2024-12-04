using System.Text.RegularExpressions;

namespace AdventOfCode24;

internal partial class Program
{
    static void Main(string[] args)
    {
        var grid = ParseGrid(Extractor.GetInput());
        var xmasList = FindXMASPatterns(grid);
        Console.WriteLine(xmasList.Count);
    }

    static List<(int row, int col, int rowDelta, int colDelta, bool backwards)> FindXMASPatterns(char[,] grid)
    {
        var xmasList = new List<(int, int, int, int, bool)>();

        for (var r = 0; r < grid.GetLength(0); r++)
        {
            for (var c = 0; c < grid.GetLength(1); c++)
            {
                if (grid[r, c] != 'X') continue;

                foreach (var (rowDelta, colDelta) in GetDirections())
                {
                    // Check forward MAS
                    if (CheckMAS(grid, r, c, rowDelta, colDelta, false))
                    {
                        xmasList.Add((r, c, rowDelta, colDelta, false));
                    }

                    // Check backward MAS
                    if (CheckMAS(grid, r, c, rowDelta, colDelta, true))
                    {
                        xmasList.Add((r, c, rowDelta, colDelta, true));
                    }
                }
            }
        }

        // Remove duplicate patterns
        return xmasList.Distinct(new XMASPatternComparer()).ToList();
    }

    static bool CheckMAS(char[,] grid, int xRow, int xCol, int rowDelta, int colDelta, bool backwards)
    {
        var masLetters = backwards ? new[] { 'S', 'A', 'M' } : new[] { 'M', 'A', 'S' };

        for (var k = 1; k < 4; k++)
        {
            var newRow = xRow + k * rowDelta;
            var newCol = xCol + k * colDelta;

            if (!IsValid(grid, newRow, newCol) || grid[newRow, newCol] != masLetters[k - 1])
                return false;
        }
        return true;
    }

    static bool IsValid(char[,] grid, int row, int col)
        => row >= 0 && row < grid.GetLength(0) && col >= 0 && col < grid.GetLength(1);

    static IEnumerable<(int rowDelta, int colDelta)> GetDirections()
    {
        return new[]
        {
            ( 0,  1),
            ( 0, -1),
            ( 1,  0),
            (-1,  0),
            ( 1,  1),
            (-1, -1),
            ( 1, -1),
            (-1,  1)
        };
    }

    // Custom comparer to remove duplicate patterns
    class XMASPatternComparer : IEqualityComparer<(int row, int col, int rowDelta, int colDelta, bool backwards)>
    {
        public bool Equals((int row, int col, int rowDelta, int colDelta, bool backwards) x,
                           (int row, int col, int rowDelta, int colDelta, bool backwards) y)
        {
            // Consider patterns the same if they cover the same 4 grid positions
            return x.row == y.row && x.col == y.col &&
                   x.rowDelta == y.rowDelta && x.colDelta == y.colDelta;
        }

        public int GetHashCode((int row, int col, int rowDelta, int colDelta, bool backwards) obj)
        {
            return HashCode.Combine(obj.row, obj.col, obj.rowDelta, obj.colDelta);
        }
    }

    static char[,] ParseGrid(string input)
    {
        string[] rows;

        input = input.Replace("\r", "");

        if (input.Contains('\n'))
        {
            rows = input.Split('\n');
        }
        else
        {
            rows = SplitIntoEqualParts(input);
        }

        var rowCount = rows.Length;
        var colCount = rows[0].Length;
        foreach (string row in rows)
        {
            if (row.Length != colCount)
            {
                throw new Exception("Rows are not of consistent length.");
            }
        }

        var grid = new char[rowCount, colCount];
        for (var r = 0; r < rowCount; r++)
        {
            for (var c = 0; c < colCount; c++)
            {
                grid[r, c] = rows[r][c];
            }
        }

        return grid;
    }

    static string[] SplitIntoEqualParts(string input)
    {
        var length = input.Length;

        var side = (int)Math.Sqrt(length);
        if (side * side != length)
        {
            throw new Exception("Input not square.");
        }

        List<string> rows = [];
        for (var i = 0; i < length; i += side)
        {
            rows.Add(input.Substring(i, side));
        }
        return rows.ToArray();
    }
}
