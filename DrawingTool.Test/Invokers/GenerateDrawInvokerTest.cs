using Autofac;
using Autofac.Extras.Moq;
using DrawingTool.MVC.Commands.Canvas;
using DrawingTool.MVC.Commands.Reader;
using DrawingTool.MVC.Commands.Writer;
using DrawingTool.MVC.Contracts.Commands.Canvas;
using DrawingTool.MVC.Contracts.Commands.Reader;
using DrawingTool.MVC.Contracts.Commands.Writer;
using DrawingTool.MVC.Invoker;
using DrawingTool.MVC.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace DrawingTool.Test.Invokers
{
    public class GenerateDrawInvokerTest
    {
        private GenerateDrawInvoker GenerateDrawInvoker { get; set; }
        public IEnumerable<DrawCommand> ExpectedCommands { get; set; }

        public GenerateDrawInvokerTest()
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

        private Action<ContainerBuilder> AutoFacConatiner()
        {
            var writeLineCommmand = new WriteLineCommand();
            Action<ContainerBuilder> containerBuilderAction = delegate (ContainerBuilder cb)
            {
                cb.RegisterInstance(new GetCommandsToDrawCommand()).As<IGetCommandsToDrawCommand>();
                cb.RegisterInstance(new GenerateCanvasCommand()).As<IGenerateCanvasCommand>();
                cb.RegisterInstance(writeLineCommmand).As<IWriteLineCommand>();
                cb.RegisterInstance(new WriteRectangleCommand(writeLineCommmand)).As<IWriteRectangleCommand>();
                cb.RegisterInstance(new FillCanvasCommand()).As<IFillCanvasCommand>();
                cb.RegisterInstance(new GetStringToWriteFromCanvasCommand()).As<IGetStringToWriteFromCanvasCommand>();
            };

            return containerBuilderAction;

        }

        [Fact]
        public void GenerateCanvasSuccess()
        {
            var builder = this.AutoFacConatiner();
            using (var mock = AutoMock.GetLoose(builder))
            {
                ///Just in case to setup custom expected commands
                //mock.Mock<IGetCommandsToDrawCommand>()
                //    .Setup(x => x.Execute(It.IsAny<IEnumerable<string>>()))
                //    .Returns(this.ExpectedCommands);

                var create = mock.Create<IGenerateCanvasCommand>();
                var writeLine = mock.Create<IWriteLineCommand>();
                var writeRectangle = mock.Create<IWriteRectangleCommand>();
                var fillCanvas = mock.Create<IFillCanvasCommand>();
                var getStringToWrite = mock.Create<IGetStringToWriteFromCanvasCommand>();

                this.GenerateDrawInvoker = mock.Create<GenerateDrawInvoker>();

                var commadsToExecute = new List<string>
                {
                    "C 20 4",
                    "L 1 2 6 2",
                    "L 6 3 6 4",
                    "R 16 1 20 3",
                    "B 10 3 o"
                };

                var result = this.GenerateDrawInvoker.Execute(commadsToExecute);
                result.Errors.Should().HaveCount(0);
            }
        }

        [Fact]
        public void CanExecuteCanvasWithoutCreation()
        {
            var builder = this.AutoFacConatiner();
            using (var mock = AutoMock.GetLoose(builder))
            {
                ///Just in case to setup custom expected commands
                //mock.Mock<IGetCommandsToDrawCommand>()
                //    .Setup(x => x.Execute(It.IsAny<IEnumerable<string>>()))
                //    .Returns(this.ExpectedCommands);

                var create = mock.Create<IGenerateCanvasCommand>();
                var writeLine = mock.Create<IWriteLineCommand>();
                var writeRectangle = mock.Create<IWriteRectangleCommand>();
                var fillCanvas = mock.Create<IFillCanvasCommand>();
                var getStringToWrite = mock.Create<IGetStringToWriteFromCanvasCommand>();

                this.GenerateDrawInvoker = mock.Create<GenerateDrawInvoker>();

                var commadsToExecute = new List<string>
                {
                    "L 1 2 6 2",
                    "L 6 3 6 4",
                    "R 16 1 20 3",
                    "B 10 3 o"
                };

                var result = this.GenerateDrawInvoker.Execute(commadsToExecute);
                Assert.True(result.Errors != null);
                result.Errors.Should().HaveCount(4);
            }
        }
    }
}
