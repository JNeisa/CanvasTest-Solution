using DrawingToolConsole.DataTrasnferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingToolConsole.Contracts.Models
{
    public interface ICommandType
    {
        CommandType Type { get; set; }
    }
}
