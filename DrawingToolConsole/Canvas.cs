using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DrawingToolConsole
{
    public class Canvas
    {
        private string[,] MainCanvas { get; set; }
        private bool[,] Visited { get; set; }

        private int Height { get; set; }

        private int Width { get; set; }

        private Queue<Point> PositionToStartValidate { get; set; }
    
        private static int[] PosiblePositionsInY = { -1, 0, 0, 1 };
        private static int[] PosiblePositionsInX = { 0, -1, 1, 0 };

        public Canvas(int x, int y)
        {
            this.Width = x + 2;
            this.Height = y + 2;
            this.MainCanvas = new string[this.Height, this.Width];
            this.Visited = new bool[this.Height, this.Width];
            this.CreateCanvas();
        }

        public void CreateCanvas()
        {
            for (int y = 0; y < this.Height; y++)
            {
                var lineBuilder = new StringBuilder();
                for (int x = 0; x < this.Width; x++)
                {
                    var textToAppend = " ";
                    if (y == 0 || y == this.Height - 1)
                    {
                        this.MainCanvas[y, x] = "-";
                        textToAppend = "-";
                    }
                    else if (y > 0 && y < this.Height - 1 && (x == 0 || x == this.Width - 1))
                    {
                        this.MainCanvas[y, x] = "|";
                        textToAppend = "|";
                    }

                    lineBuilder.Append(textToAppend);
                }
                Console.WriteLine(lineBuilder.ToString());
            }
        }

        public void WriteLineCanvas(int x1, int y1, int x2, int y2)
        {
            for (int i = 0; i < this.MainCanvas.GetLength(0); i++)
            {
                var lineBuilder = new StringBuilder();
                for (int j = 0; j < this.MainCanvas.GetLength(1); j++)
                {
                    var textToAppend = " ";
                    if (!string.IsNullOrEmpty(this.MainCanvas[i, j]))
                    {
                        textToAppend = this.MainCanvas[i, j];
                    }
                    else if((i == y1 || i == y2) && j >= x1 && j <= x2)
                    {
                        this.MainCanvas[i, j] = "x";
                        textToAppend = this.MainCanvas[i, j];
                    }

                    lineBuilder.Append(textToAppend);
                }
                Console.WriteLine(lineBuilder.ToString());
            }
        }

        public void WriteRectangleCanvas(int x1, int y1, int x2, int y2)
        {
            for (int y = 0; y < this.MainCanvas.GetLength(0); y++)
            {
                var lineBuilder = new StringBuilder();
                for (int x = 0; x < this.MainCanvas.GetLength(1); x++)
                {
                    var textToAppend = " ";
                    if (!string.IsNullOrEmpty(this.MainCanvas[y, x]))
                    {
                        textToAppend = this.MainCanvas[y, x];
                    }
                    else if (y >= y1 || y <= y2)
                    {
                        if ((y == y1 || y == y2) && x >= x1 && x <= x2)
                        {
                            this.MainCanvas[y, x] = "x";
                            textToAppend = this.MainCanvas[y, x];
                        }
                        else if (y > y1 && y < y2 && (x == x1 || x == x2))
                        {
                            this.MainCanvas[y, x] = "x";
                            textToAppend = this.MainCanvas[y, x];
                        }
                    }

                    lineBuilder.Append(textToAppend);
                }
                Console.WriteLine(lineBuilder.ToString());
            }
        }

        private bool MarkAsValid(int x, int y, string character)
        {
            var result = x >= 0 && x < this.Width && y >= 0 && y < this.Height && string.IsNullOrEmpty(this.MainCanvas[y, x]) && !this.Visited[y, x];
            if (result)
            {
                this.MainCanvas[y, x] = character;
            }
            return result;
        }

        public void FillSpaces(int x1, int y1)
        {
            this.PositionToStartValidate = new Queue<Point>();
            this.PositionToStartValidate.Enqueue(new Point { X = x1, Y = y1 });

            while (this.PositionToStartValidate.Any())
            {
                var next = this.PositionToStartValidate.Dequeue();
                for (int i = 0; i < 4; i++)
                {
                    if (MarkAsValid(next.X + PosiblePositionsInX[i], next.Y + PosiblePositionsInY[i], "O"))
                    {
                        this.Visited[next.Y + PosiblePositionsInY[i], next.X + PosiblePositionsInX[i]] = true;
                        this.PositionToStartValidate.Enqueue(new Point
                        {
                            X = next.X + PosiblePositionsInX[i],
                            Y = next.Y + PosiblePositionsInY[i]
                        });
                    }
                }
            }

            for (int i = 0; i < this.MainCanvas.GetLength(0); i++)
            {
                var lineBuilder = new StringBuilder();
                for (int j = 0; j < this.MainCanvas.GetLength(1); j++)
                {
                    if (!string.IsNullOrEmpty(this.MainCanvas[i, j]))
                        lineBuilder.Append(this.MainCanvas[i, j]);
                    else
                        lineBuilder.Append(" ");
                    
                }
                Console.WriteLine(lineBuilder.ToString());
            }
        }
    }
}
