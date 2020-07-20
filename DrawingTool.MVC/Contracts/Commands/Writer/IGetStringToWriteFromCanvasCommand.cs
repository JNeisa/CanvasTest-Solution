namespace DrawingTool.MVC.Contracts.Commands.Writer
{
    public interface IGetStringToWriteFromCanvasCommand
    {
        string[] Execute(string[,] canvas);
    }
}