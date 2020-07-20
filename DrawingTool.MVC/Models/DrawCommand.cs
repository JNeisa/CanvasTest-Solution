namespace DrawingTool.MVC.Models
{
    public class DrawCommand
    {
        public DrawCommandType Type { get; set; }

        public Point Source { get; set; }

        public Point Destination { get; set; }

        public string Character { get; set; }

        public string SourceCommandLine { get; set; }
    }
}