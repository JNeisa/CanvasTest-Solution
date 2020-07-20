using DrawingTool.MVC.Models;

namespace DrawingTool.MVC.Contracts.Commands.Canvas
{
    public interface IWriteLineCommand
    {
        string[,] Execute(string[,] canvas, Point sourcePoint, Point destinationPoint);
    }
}
