using DrawingTool.MVC.DataTransferObjects;
using DrawingTool.MVC.ViewModels;
using System.Collections.Generic;

namespace DrawingTool.MVC.Contracts.Invokers
{
    public interface IGenerateDrawInvoker
    {
        DrawResultDTO Execute(DrawViewModel drawViewModel);

        DrawResultDTO Execute(IEnumerable<string> commandsList);
    }
}