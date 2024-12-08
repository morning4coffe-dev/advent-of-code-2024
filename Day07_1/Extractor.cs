namespace AdventOfCode24;

internal class Extractor
{
    public static string[] GetInput()
    {
        var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var projectDirectory = Directory.GetParent(currentDirectory)?.Parent?.Parent?.Parent?.FullName;

        var filePath = Path.Combine(projectDirectory, "input.txt");

        return File.ReadAllLines(filePath);
    }
}
