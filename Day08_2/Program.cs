namespace AdventOfCode24;

internal class Program
{
    static void Main(string[] args)
    {
        var input = Extractor.GetInput();
        int width = input[0].Length;
        int height = input.Length;
        var grid = new char[width, height];
        var locations = new Dictionary<char, List<(int x, int y)>>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var currentInput = input[y][x];
                if (currentInput != '.')
                {
                    if (locations.ContainsKey(currentInput))
                    {
                        locations[currentInput].Add((x, y));
                    }
                    else
                    {
                        locations.Add(currentInput, [(x, y)]);
                    }
                }
                grid[x, y] = currentInput;
            }
        }

        var antinodes = new HashSet<(int x, int y)>();

        foreach (var type in locations)
        {
            var location = type.Value;
            foreach (var antenna in location)
            {
                antinodes.Add(antenna);
            }

            for (int i = 0; i < location.Count; i++)
            {
                for (int j = i + 1; j < location.Count; j++)
                {
                    (int x, int y) distance = (location[j].x - location[i].x, location[j].y - location[i].y);

                    var currentLocation = location[i];
                    while (true)
                    {
                        currentLocation = (currentLocation.x + distance.x, currentLocation.y + distance.y);
                        if (!IsWithinBounds(currentLocation)) break;
                        antinodes.Add(currentLocation);
                    }

                    currentLocation = location[i];
                    while (IsWithinBounds(currentLocation))
                    {
                        currentLocation = (currentLocation.x - distance.x, currentLocation.y - distance.y);
                        if (!IsWithinBounds(currentLocation)) break;
                        antinodes.Add(currentLocation);
                    }
                }
            }
        }

        Console.WriteLine(antinodes.Count);

        bool IsWithinBounds((int x, int y) location)
        {
            return location.x >= 0 && location.x < width && location.y >= 0 && location.y < height;
        }
    }
}