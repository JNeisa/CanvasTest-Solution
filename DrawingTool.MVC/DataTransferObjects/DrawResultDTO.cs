using System.Collections.Generic;

namespace DrawingTool.MVC.DataTransferObjects
{
    public class DrawResultDTO
    {
        public byte[] FileContent { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}