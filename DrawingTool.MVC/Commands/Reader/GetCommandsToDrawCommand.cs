using DrawingTool.MVC.Contracts.Commands.Reader;
using DrawingTool.MVC.DataTransferObjects;
using DrawingTool.MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using WebGrease.Css.Extensions;

namespace DrawingTool.MVC.Commands.Reader
{
    public class GetCommandsToDrawCommand : IGetCommandsToDrawCommand
    {
        private string Error { get; set; }
        public DrawCommandsResultDTO Execute(Stream stream)
        {
            var result = new DrawCommandsResultDTO();
            var drawCommands = new List<DrawCommand>();
            var errorCommands = new List<string>();

            using (StreamReader reader = new StreamReader(stream))
            {
                do
                {
                    string commandLine = reader.ReadLine();
                    var commandToAdd = this.GetCommand(commandLine);
                    if (commandToAdd != null)
                        drawCommands.Add(commandToAdd);
                    else
                        errorCommands.Add(this.Error);

                } while (reader.Peek() != -1);
            }

            result.CommandsWithErrors = errorCommands;
            result.DrawCommands = drawCommands;
            return result;
        }

        public DrawCommandsResultDTO Execute(IEnumerable<string> listOfCommands)
        {
            var result = new DrawCommandsResultDTO();
            var drawCommands = new List<DrawCommand>();
            var errorCommands = new List<string>();

            listOfCommands.ForEach((x) => 
            {
                var command = this.GetCommand(x);
                if (command != null)
                    drawCommands.Add(command);
                else
                    errorCommands.Add(this.Error);
            });

            result.CommandsWithErrors = errorCommands;
            result.DrawCommands = drawCommands;
            return result;
        }

        private DrawCommand GetCommand(string drawCommandLine)
        {
            DrawCommand commandToAdd = null;
            try
            {
                var commandOptions = drawCommandLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (Enum.TryParse(commandOptions[0], out DrawCommandType drawCommandType))
                {
                    switch (drawCommandType)
                    {
                        case DrawCommandType.C:
                            commandToAdd = new DrawCommand
                            {
                                Type = DrawCommandType.C,
                                Source = new Point(commandOptions[1], commandOptions[2]),
                                SourceCommandLine = drawCommandLine
                            };
                            break;
                        case DrawCommandType.L:
                        case DrawCommandType.R:
                            commandToAdd = new DrawCommand
                            {
                                Type = drawCommandType,
                                Source = new Point(commandOptions[1], commandOptions[2]),
                                Destination = new Point(commandOptions[3], commandOptions[4]),
                                SourceCommandLine = drawCommandLine
                            };
                            break;
                        case DrawCommandType.B:
                            commandToAdd = new DrawCommand
                            {
                                Type = DrawCommandType.B,
                                Source = new Point(commandOptions[1], commandOptions[2]),
                                Character = commandOptions[3],
                                SourceCommandLine = drawCommandLine
                            };
                            break;
                        default:
                            this.Error = $"The system can't recognize the command: {drawCommandLine}";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Error = $"The can't process the following command: {drawCommandLine}, error: {ex.Message}";
            }

            return commandToAdd;
        }
    }
}