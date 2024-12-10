using System.Text;

namespace AdventOfCode24;

internal class Program
{
    static void Main(string[] args)
    {
        var input = Extractor.GetInput();
        var disk = new StringBuilder();
        var fileId = 0;

        for (var i = 0; i < input.Length; i++)
        {
            var length = input[i] - '0';
            var symbol = (i % 2 == 0) ? (char)('0' + fileId++) : '.';

            for (var j = 0; j < length; j++)
            {
                disk.Append(symbol);
            }
        }

        for (var i = disk.Length - 1; i >= 0; i--)
        {
            if (disk[i] == '.')
            {
                continue;
            }

            var firstDot = disk.ToString().IndexOf('.');
            if (firstDot >= i)
            {
                break;
            }

            var charToMove = disk[i];
            disk.Remove(i, 1);
            disk[firstDot] = charToMove;
        }

        var checksum = 0L;
        for (var i = 0; i < disk.Length; i++)
        {
            if (disk[i] != '.')
            {
                checksum += (disk[i] - '0') * i;
            }
        }

        Console.WriteLine(checksum);
    }
}