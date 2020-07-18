using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrawingToolConsole
{
    public class Canvas
    {
        private string[,] MainCanvas { get; set; }
        private bool[,] Visited { get; set; }

        private int Height { get; set; }

        private int Width { get; set; }

        private Queue<Position> PositionToStartValidate { get; set; }

        private static int[] PosiblePositionsInY = { -1, 0, 0, 1 };
        private static int[] PosiblePositionsInX = { 0, -1, 1, 0 };

        public Canvas(int x, int y)
        {
            this.Width = x + 1;
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
            x1 = x1 - 1;
            x2 = x2 - 1;
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
                    else if (i >= y1 || i <= y2)
                    {
                        if ((i == y1 || i == y2) && j >= x1 && j <= x2)
                        {
                            this.MainCanvas[i, j] = "x";
                            textToAppend = this.MainCanvas[i, j];
                        }
                        else if (i > y1 && i < y2 && (j == x1 || j == x2))
                        {
                            this.MainCanvas[i, j] = "x";
                            textToAppend = this.MainCanvas[i, j];
                        }
                    }

                    lineBuilder.Append(textToAppend);
                }
                Console.WriteLine(lineBuilder.ToString());
            }
        }

        private bool MarkAsValid(string[,] canvas, int x, int y, string character)
        {
            var result = x >= 0 && x < this.Width && y >= 0 && y < this.Height && string.IsNullOrEmpty(canvas[y, x]) && !this.Visited[y, x];
            if (result)
            {
                canvas[y, x] = character;
            }
            return result;
        }

        public string[,] FillSpaces(int x1, int y1)
        {
            this.PositionToStartValidate = new Queue<Position>();
            this.PositionToStartValidate.Enqueue(new Position { X = x1, Y = y1 });

            while (this.PositionToStartValidate.Any())
            {
                var next = this.PositionToStartValidate.Dequeue();
                for (int i = 0; i < 4; i++)
                {
                    if (MarkAsValid(this.MainCanvas, next.X + PosiblePositionsInX[i], next.Y + PosiblePositionsInY[i], "0"))
                    {
                        this.Visited[next.Y + PosiblePositionsInY[i], next.X + PosiblePositionsInX[i]] = true;
                        this.PositionToStartValidate.Enqueue(new Position
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

            return this.MainCanvas;
        }
    }
}
