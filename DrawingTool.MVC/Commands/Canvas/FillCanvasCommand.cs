using DrawingTool.MVC.Contracts.Commands.Canvas;
using DrawingTool.MVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace DrawingTool.MVC.Commands.Canvas
{
    public class FillCanvasCommand : IFillCanvasCommand
    {
        public string[,] MainCanvas { get; set; }
        private Queue<Point> PositionToStartValidate { get; set; }

        private bool[,] Visited { get; set; }

        private static int[] PosiblePositionsInY = { -1, 0, 0, 1 };

        private static int[] PosiblePositionsInX = { 0, -1, 1, 0 };

        private bool MarkAsValid(int x, int y, string character)
        {
            var result = x >= 0 && x < this.MainCanvas.GetLength(1) && y >= 0 && y < this.MainCanvas.GetLength(0) && string.IsNullOrEmpty(this.MainCanvas[y, x]) && !this.Visited[y, x];
            if (result) this.MainCanvas[y, x] = character;
            return result;
        }

        public string[,] Execute(Point pointToStartFill, string character)
        {
            this.PositionToStartValidate = new Queue<Point>();
            this.PositionToStartValidate.Enqueue(pointToStartFill);
            this.Visited = new bool[this.MainCanvas.GetLength(0), this.MainCanvas.GetLength(1)];

            while (this.PositionToStartValidate.Any())
            {
                var next = this.PositionToStartValidate.Dequeue();
                for (int i = 0; i < 4; i++)
                {
                    if (MarkAsValid(next.X + PosiblePositionsInX[i], next.Y + PosiblePositionsInY[i], character))
                    {
                        this.Visited[next.Y + PosiblePositionsInY[i], next.X + PosiblePositionsInX[i]] = true;
                        this.PositionToStartValidate.Enqueue(new Point(next.X + PosiblePositionsInX[i], next.Y + PosiblePositionsInY[i]));
                    }
                }
            }

            return this.MainCanvas;
        }
    }
}