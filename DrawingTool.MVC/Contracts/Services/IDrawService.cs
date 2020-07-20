using DrawingTool.MVC.DataTransferObjects;
using DrawingTool.MVC.ViewModels;

namespace DrawingTool.MVC.Contracts.Services
{
    public interface IDrawService
    {
        DrawResultDTO GenerateDrawFromCommands(DrawViewModel drawViewModel);
    }
}
