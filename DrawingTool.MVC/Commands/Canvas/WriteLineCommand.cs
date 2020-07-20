using DrawingTool.MVC.Contracts.Commands.Canvas;
using DrawingTool.MVC.Models;
using System;

namespace DrawingTool.MVC.Commands.Canvas
{
    public class WriteLineCommand : IWriteLineCommand
    {
        public string[,] MainCanvas { get; set; }
        public string[,] Execute(string[,] canvas, Point sourcePoint, Point destinationPoint)
        {
            this.MainCanvas = canvas;
            if (sourcePoint.X.Equals(destinationPoint.X))
            {
                var source = Math.Min(sourcePoint.Y, destinationPoint.Y); 
                var destination = Math.Max(sourcePoint.Y, destinationPoint.Y);
                for (int i = source; i <= destination; i++)
                {
                    if (string.IsNullOrEmpty(canvas[i, sourcePoint.X]))
                    {
                        canvas[i, sourcePoint.X] = "x";
                    }
                }
            }

            if (sourcePoint.Y.Equals(destinationPoint.Y))
            {
                var source = Math.Min(sourcePoint.X, destinationPoint.X);
                var destination = Math.Max(sourcePoint.X, destinationPoint.X);
                for (int i = source; i <= destination; i++)
                {
                    if (string.IsNullOrEmpty(canvas[sourcePoint.Y, i]))
                    {
                        canvas[sourcePoint.Y, i] = "x";
                    }
                }
            }

            return canvas;
        }
    }
}