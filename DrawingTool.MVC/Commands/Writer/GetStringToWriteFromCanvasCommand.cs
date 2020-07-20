using DrawingTool.MVC.Contracts.Commands.Writer;
using System.Collections.Generic;
using System.Linq;

namespace DrawingTool.MVC.Commands.Writer
{
    public class GetStringToWriteFromCanvasCommand : IGetStringToWriteFromCanvasCommand
    {
        public string[] Execute(string[,] canvas)
        {
            if (canvas != null)
            {
                var result = new List<string>();
                for (int i = 0; i < canvas.GetLength(0); i++)
                {
                    result.Add(
                        string.Join(
                        "",
                        Enumerable
                        .Range(0, canvas.GetLength(1))
                        .Select(column => this.ValidateBlankSpace(canvas[i, column]))
                        ));
                }

                return result.ToArray();
            }

            return null;
        }

        private string ValidateBlankSpace(string value)
        {
            return string.IsNullOrEmpty(value) ? " " : value;
        }
    }
}