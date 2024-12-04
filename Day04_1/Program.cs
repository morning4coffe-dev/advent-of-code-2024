namespace AdventOfCode24;

internal partial class Program
{
    private static int _width;
    private static int _height;
    private static char[,] _map;

    static void Main(string[] args)
    {
        var input = Extractor.GetInput();
        ParseGrid(input);

        int score = 0;

        var directions = new (int X, int Y)[]
        {
            (1, 1),
            (-1, -1),
            (1, -1),
            (-1, 1)
        };

        for (var x = 0; x < _width; x++)
        {
            for (var y = 0; y < _height; y++)
            {
                var diagonalCount = 0;

                foreach (var direction in directions)
                {
                    var oppositeDirection = (-direction.X, -direction.Y);
                    var startingPoint = (x + oppositeDirection.Item1, y + oppositeDirection.Item2);

                    if (SearchWord(startingPoint, direction, "MAS"))
                    {
                        diagonalCount++;
                    }
                }

                if (diagonalCount == 2)
                {
                    score++;
                }
            }
        }

        Console.WriteLine(score);
    }

    private static void ParseGrid(string input)
    {
        var lines = new List<string>(input.Replace("\r", "").Split('\n'));

        _height = lines.Count;
        _width = lines[0].Length;

        _map = new char[_width, _height];
        for (var y = 0; y < _height; y++)
        {
            for (var x = 0; x < _width; x++)
            {
                _map[x, y] = lines[y][x];
            }
        }
    }

    private static bool SearchWord((int X, int Y) startingPoint, (int X, int Y) direction, string word)
    {
        for (var i = 0; i < word.Length; i++)
        {
            var position = (X: startingPoint.X + direction.X * i, Y: startingPoint.Y + direction.Y * i);

            if (position.X < 0 || position.X >= _width ||
                position.Y < 0 || position.Y >= _height ||
                _map[position.X, position.Y] != word[i])
            {
                return false;
            }
        }

        return true;
    }
}
