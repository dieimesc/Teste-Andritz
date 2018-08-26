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
            try
            {
                StreamReader sr = new StreamReader(input, encoding);
                StreamWriter sw = new StreamWriter(output, encoding);
                                
                int count = 0;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();

                    if (int.TryParse(line.Substring(0, line.Length), out count))
                    {
                        sw.WriteLine(count.ToString());count++;continue;


                    }
                    else if (line.IndexOf(":") == 2)
                    {

                     
                        TimeSpan timeOriginalInicio = TimeSpan.Parse(line.Substring(0, line.Length - line.IndexOf("-->") - 3).Trim().Replace(",", "."));
                        TimeSpan timeOriginalFinal = TimeSpan.Parse(line.Substring(line.IndexOf("-->") + 3, line.Length - line.IndexOf("-->") - 3)
                            .Trim().Replace(",", "."));

                       TimeSpan timeSpanTargetInicio =   timeOriginalInicio.Add(timeSpan);
                       TimeSpan timeSpanTargetFim =  timeOriginalFinal.Add(timeSpan);

                        string lineToOutput=timeSpanTargetInicio.ToString(@"hh\:mm\:ss\.fff") + " --> " + timeSpanTargetFim.ToString(@"hh\:mm\:ss\.fff");

                        sw.WriteLine(lineToOutput);

                    }
                    else
                        sw.WriteLine(line);
                      
                    
                   
                }
                sw.Flush();
                StreamReader sre = new StreamReader(sw.BaseStream);
                output = GenerateStreamFromString(sre.ReadToEnd());
            
                
            }
            
            catch (Exception e)
            { 
                


            }


        }
        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
