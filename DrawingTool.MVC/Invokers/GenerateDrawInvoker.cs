using DrawingTool.MVC.Contracts.Commands.Canvas;
using DrawingTool.MVC.Contracts.Commands.Reader;
using DrawingTool.MVC.Contracts.Commands.Writer;
using DrawingTool.MVC.Contracts.Invokers;
using DrawingTool.MVC.DataTransferObjects;
using DrawingTool.MVC.Models;
using DrawingTool.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DrawingTool.MVC.Invoker
{
    public class GenerateDrawInvoker : IGenerateDrawInvoker
    {
        public string[,] MainCanvas { get; set; }

        private readonly IGetCommandsToDrawCommand getCommandsToDrawCommand;
        private readonly IGenerateCanvasCommand generateCanvasCommand;
        private readonly IWriteLineCommand writeCanvasLineCommand;
        private readonly IWriteRectangleCommand writeRectangleCommand;
        private readonly IFillCanvasCommand fillCanvasCommand;
        private readonly IGetStringToWriteFromCanvasCommand getStringToWriteFromCanvasCommand;
        private readonly IWriteFileToResponseCommand writeFileToResponseCommand;

        public GenerateDrawInvoker(
            IGetCommandsToDrawCommand getCommandsToDrawCommand,
            IGenerateCanvasCommand generateCanvasCommand,
            IWriteLineCommand writeCanvasLineCommand,
            IWriteRectangleCommand writeRectangleCommand,
            IFillCanvasCommand fillCanvasCommand,
            IGetStringToWriteFromCanvasCommand getStringToWriteFromCanvasCommand,
            IWriteFileToResponseCommand writeFileToResponseCommand)
        {
            this.getCommandsToDrawCommand = getCommandsToDrawCommand;
            this.generateCanvasCommand = generateCanvasCommand;
            this.writeCanvasLineCommand = writeCanvasLineCommand;
            this.writeRectangleCommand = writeRectangleCommand;
            this.fillCanvasCommand = fillCanvasCommand;
            this.getStringToWriteFromCanvasCommand = getStringToWriteFromCanvasCommand;
            this.writeFileToResponseCommand = writeFileToResponseCommand;
        }

        public DrawResultDTO Execute(DrawViewModel drawViewModel)
        {
            var commandsToExecute = this.getCommandsToDrawCommand.Execute(drawViewModel.DrawCommands.InputStream);
           return this.ExecuteCommands(commandsToExecute.DrawCommands, commandsToExecute.CommandsWithErrors);
        }

        public DrawResultDTO Execute(IEnumerable<string> commandsList)
        {
            var commandsToExecute = this.getCommandsToDrawCommand.Execute(commandsList);
            return this.ExecuteCommands(commandsToExecute.DrawCommands, commandsToExecute.CommandsWithErrors);
        }

        private DrawResultDTO ExecuteCommands(IEnumerable<DrawCommand> commandsToExecute, IEnumerable<string> previousErrors)
        {
            var result = new DrawResultDTO();
            List<string> lines = new List<string>();
            List<string> errors = new List<string>();
            if (previousErrors != null && previousErrors.Any())
            {
                errors.AddRange(previousErrors);
            }

            foreach (var command in commandsToExecute)
            {
                try
                {
                    if (this.MainCanvas == null && command.Type.Equals(DrawCommandType.C))
                    {
                        this.MainCanvas = this.generateCanvasCommand.Execute(command.Source);
                    }
                    else if (this.MainCanvas != null)
                    {
                        switch (command.Type)
                        {
                            case DrawCommandType.C:
                                this.MainCanvas = this.generateCanvasCommand.Execute(command.Source);
                                break;
                            case DrawCommandType.L:
                                this.writeCanvasLineCommand.Execute(this.MainCanvas, command.Source, command.Destination);
                                break;
                            case DrawCommandType.R:
                                this.writeRectangleCommand.Execute(this.MainCanvas, command.Source, command.Destination);
                                break;
                            case DrawCommandType.B:
                                this.fillCanvasCommand.MainCanvas = this.MainCanvas;
                                this.fillCanvasCommand.Execute(command.Source, command.Character);
                                break;
                        }
                    }
                    else
                    {
                        var error = $"fail while executing the following command: {command.SourceCommandLine} because doesn't exist a previous canvas";
                        lines.Add(error);
                        errors.Add(error);
                    }
                }
                catch (Exception ex)
                {
                    var error = $"fail while executing the following command: {command.SourceCommandLine}, error: {ex.Message}";
                    lines.Add(error);
                    errors.Add(error);
                }

                var linesToAdd = this.getStringToWriteFromCanvasCommand.Execute(this.MainCanvas);
                if (linesToAdd != null)
                {
                    lines.AddRange(linesToAdd);
                }
                #if DEBUG
                lines.ForEach(x => Debug.WriteLine(x));
                #endif
            }

            result.FileContent = this.writeFileToResponseCommand.Execute(lines.ToList());
            result.Errors = errors;
            return result;
        }
    }
}