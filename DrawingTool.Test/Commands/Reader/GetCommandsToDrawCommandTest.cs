using DrawingTool.MVC.Commands.Reader;
using DrawingTool.MVC.Models;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace DrawingTool.Test.Commands.Reader
{
    public class GetCommandsToDrawCommandTest
    {
        public IEnumerable<DrawCommand> ExpectedCommands { get; set; }

        private GetCommandsToDrawCommand getCommandsToDrawCommand { get; set; }
        public GetCommandsToDrawCommandTest()
        {
            this.ExpectedCommands = new List<DrawCommand>
            {
                new DrawCommand
                {
                    Type = DrawCommandType.C,
                    Source =  new Point(20, 4),
                    SourceCommandLine = "C 20 4"
                },
                new DrawCommand
                {
                    Type = DrawCommandType.L,
                    Source =  new Point(1, 2),
                    Destination = new Point(6, 2),
                    SourceCommandLine = "L 1 2 6 2"
                },
                new DrawCommand
                {
                    Type = DrawCommandType.L,
                    Source =  new Point(6, 3),
                    Destination = new Point(6, 4),
                    SourceCommandLine = "L 6 3 6 4"
                },
                new DrawCommand
                {
                    Type = DrawCommandType.R,
                    Source =  new Point(16, 1),
                    Destination = new Point(20, 3),
                    SourceCommandLine = "R 16 1 20 3"
                },
                new DrawCommand
                {
                    Type = DrawCommandType.B,
                    Source =  new Point(10, 3),
                    Character = "o",
                    SourceCommandLine = "B 10 3 o"
                }
            };
        }

        [Fact]
        public void GetCorrectCommands()
        {
            var commadsToAdd = new List<string>
            {
                "C 20 4",
                "L 1 2 6 2",
                "L 6 3 6 4",
                "R 16 1 20 3",
                "B 10 3 o"
            };

            this.getCommandsToDrawCommand = new GetCommandsToDrawCommand();
            var commands = this.getCommandsToDrawCommand.Execute(commadsToAdd);
            commands.DrawCommands.Should().BeEquivalentTo(this.ExpectedCommands);
        }

        [Fact]
        public void AvoidCommandsThatNotRecognize()
        {
            var commadsToAdd = new List<string>
            {
                "C 20 4",
                "L 1 2 6 2",
                "T 1 2 6 2",
                "L 6 3 6 4",
                "J 6 3 6 4",
                "R 16 1 20 3",
                "Z 16 1 20 3",
                "B 10 3 o"
            };

            this.getCommandsToDrawCommand = new GetCommandsToDrawCommand();

            var commands = this.getCommandsToDrawCommand.Execute(commadsToAdd);
            commands.DrawCommands.Should().BeEquivalentTo(this.ExpectedCommands);
        }
    }
}
