using DrawingTool.MVC.Models;

namespace DrawingTool.MVC.Contracts.Commands.Canvas
{
    public interface IFillCanvasCommand
    {
        string[,] MainCanvas { get; set; }

        string[,] Execute(Point pointToStartFill, string character);
    }
}
