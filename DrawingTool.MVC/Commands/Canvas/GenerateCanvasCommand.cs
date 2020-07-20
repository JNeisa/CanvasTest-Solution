using DrawingTool.MVC.Contracts.Commands.Canvas;
using DrawingTool.MVC.Models;

namespace DrawingTool.MVC.Commands.Canvas
{
    public class GenerateCanvasCommand : IGenerateCanvasCommand
    {
        private int? Height { get; set; }
        private int? Width { get; set; }
            
        public string[,] Execute(Point canvasSize)
        {
            this.Width = canvasSize.X + 2;
            this.Height = canvasSize.Y + 2;

            return this.Execute(new string[this.Height.Value, this.Width.Value]);
        }

        public string[,] Execute(string[,] canvas)
        {
            if (!this.Height.HasValue || !this.Width.HasValue)
            {
                this.Height = canvas.GetLength(0);
                this.Width = canvas.GetLength(1);
            }

            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    if (y == 0 || y == this.Height - 1)
                        canvas[y, x] = "-";
                    else if (y > 0 && y < this.Height - 1 && (x == 0 || x == this.Width - 1))
                        canvas[y, x] = "|";
                }
            }

            return canvas;
        }
    }
}