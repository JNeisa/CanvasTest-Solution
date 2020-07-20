using DrawingTool.MVC.DataTransferObjects;
using System.Collections.Generic;
using System.IO;

namespace DrawingTool.MVC.Contracts.Commands.Reader
{
    public interface IGetCommandsToDrawCommand
    {
        DrawCommandsResultDTO Execute(Stream stream);

        DrawCommandsResultDTO Execute(IEnumerable<string> listOfCommands);
    }
}
