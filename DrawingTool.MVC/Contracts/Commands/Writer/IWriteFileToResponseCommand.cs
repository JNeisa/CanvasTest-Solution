using System.Collections.Generic;

namespace DrawingTool.MVC.Contracts.Commands.Writer
{
    public interface IWriteFileToResponseCommand
    {
        byte[] Execute(IEnumerable<string> linesToWrite);
    }
}