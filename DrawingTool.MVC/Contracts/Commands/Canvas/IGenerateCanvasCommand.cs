using DrawingTool.MVC.Models;

namespace DrawingTool.MVC.Contracts.Commands.Canvas
{
    public interface IGenerateCanvasCommand
    {
        string[,] Execute(Point canvasSize);

        string[,] Execute(string[,] canvas);
    }
}
