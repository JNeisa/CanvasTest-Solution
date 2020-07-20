using DrawingTool.MVC.Contracts.Commands.Writer;
using System.Collections.Generic;
using System.IO;

namespace DrawingTool.MVC.Commands.Writer
{
    public class WriteFileToResponseCommand : IWriteFileToResponseCommand
    {
        public byte[] Execute(IEnumerable<string> linesToWrite)
        {
            byte[] textFile = null;
            using (var stream = new MemoryStream())
            {
                StreamWriter objstreamwriter = new StreamWriter(stream);
                foreach (var line in linesToWrite)
                {
                    objstreamwriter.WriteLine(line);
                }
                objstreamwriter.Flush();
                objstreamwriter.Close();

                textFile = stream.ToArray();
            }

            return textFile;
        }
    }
}