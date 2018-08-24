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
            StreamWriter sw = new StreamWriter(output,encoding);

            while(!sr.EndOfStream)
            {
                string line = sr.ReadLine();

            }




        }
    }
}
