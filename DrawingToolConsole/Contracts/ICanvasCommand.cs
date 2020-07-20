using System.Collections.Generic;

namespace DrawingToolConsole.Contracts
{
    public interface ICanvasCommand
    {
        IEnumerable<Point> Points { get; set; }

        bool Execute(Point point);
    }
}
