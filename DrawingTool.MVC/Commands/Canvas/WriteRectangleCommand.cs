using DrawingTool.MVC.Contracts.Commands.Canvas;
using DrawingTool.MVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace DrawingTool.MVC.Commands.Canvas
{
    public class WriteRectangleCommand : IWriteRectangleCommand
    {
        private readonly IWriteLineCommand writeLineCommand;

        public WriteRectangleCommand(IWriteLineCommand writeLineCommand)
        {
            this.writeLineCommand = writeLineCommand;
        }

        public string[,] Execute(string[,] canvas, Point sourcePoint, Point destinationPoint)
        {
            var corners = new List<Point>();
            corners.Add(sourcePoint);
            corners.Add(new Point(sourcePoint.X, destinationPoint.Y));
            corners.Add(new Point(destinationPoint.X, sourcePoint.Y));
            corners.Add(destinationPoint);

            foreach (var item in corners.GroupBy(x => x.X))
            {
                canvas = this.writeLineCommand.Execute(canvas, item.First(), item.Last());
            }

            foreach (var item in corners.GroupBy(x => x.Y))
            {
                canvas = this.writeLineCommand.Execute(canvas, item.First(), item.Last());
            }

            return canvas;
        }
    }
}