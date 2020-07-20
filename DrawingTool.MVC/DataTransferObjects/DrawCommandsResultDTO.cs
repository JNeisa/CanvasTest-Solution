using DrawingTool.MVC.Models;
using System.Collections.Generic;

namespace DrawingTool.MVC.DataTransferObjects
{
    public class DrawCommandsResultDTO
    {
        public IEnumerable<DrawCommand> DrawCommands { get; set; }

        public IEnumerable<string> CommandsWithErrors { get; set; }
    }
}