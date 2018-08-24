using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SubtitleTimeshift
{
     public class Shifter
    {
        async static public Task Shift(Stream input, Stream output, TimeSpan timeSpan, Encoding encoding, int bufferSize = 1024, bool leaveOpen = false)
        {
            StreamReader sr = new StreamReader(input, encoding);
            StreamWriter sw = new StreamWriter(output, encoding);
            int count = 0;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();

                if (int.TryParse(line.Substring(0, line.Length), out count))
                {
                    sw.WriteLine(count.ToString());
                    sw.WriteLine(TimeSpan.FromMilliseconds(Convert.ToDouble(sr.ReadLine())) + timeSpan);
                }
            }
        }
    }
}
