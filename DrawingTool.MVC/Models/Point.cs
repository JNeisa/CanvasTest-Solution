namespace DrawingTool.MVC.Models
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(string x, string y)
        {
            X = int.Parse(x);
            Y = int.Parse(y);
        }
    }
}