using System;

namespace DrawingToolConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var canvas = new Canvas(20, 4);
            canvas.WriteLineCanvas(1, 2, 6, 2);
            canvas.WriteLineCanvas(6, 3, 6, 4);
            canvas.WriteRectangleCanvas(16, 1, 20, 3);
            canvas.FillSpaces(10, 3);
            Console.ReadKey();
        }
    }
}
