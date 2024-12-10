using System.Text;

namespace AdventOfCode24;

internal class Program
{
    static void Main(string[] args)
    {
        var input = Extractor.GetInput();
        var disk = new StringBuilder();

        var files = new List<(int Id, int Start, int Length)>();
        var fileId = 0;

        for (var i = 0; i < input.Length; i++)
        {
            var length = input[i] - '0';
            var symbol = (i % 2 == 0) ? (char)('0' + fileId++) : '.';

            if (symbol != '.')
            {
                files.Add((fileId - 1, disk.Length, length));
            }

            for (var j = 0; j < length; j++)
            {
                disk.Append(symbol);
            }
        }

        files = files.OrderByDescending(f => f.Id).ToList();

        foreach (var (id, start, length) in files)
        {
            var spanStart = -1;
            for (var i = 0; i <= disk.Length - length; i++)
            {
                if (IsSpanFree(disk, i, length))
                {
                    spanStart = i;
                    break;
                }
            }

            if (spanStart != -1 && spanStart < start)
            {
                for (var i = start; i < start + length; i++)
                {
                    disk[i] = '.';
                }

                for (var i = 0; i < length; i++)
                {
                    disk[spanStart + i] = (char)('0' + id);
                }
            }
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

    static bool IsSpanFree(StringBuilder disk, int start, int length)
    {
        for (var i = start; i < start + length; i++)
        {
            if (disk[i] != '.')
            {
                return false;
            }
        }
        return true;
    }
}
