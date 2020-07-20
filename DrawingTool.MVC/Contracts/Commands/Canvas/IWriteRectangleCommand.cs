using DrawingTool.MVC.Models;

namespace DrawingTool.MVC.Contracts.Commands.Canvas
{
    public interface IWriteRectangleCommand
    {
        string[,] Execute(string[,] canvas, Point sourcePoint, Point destinationPoint);
    }
}
