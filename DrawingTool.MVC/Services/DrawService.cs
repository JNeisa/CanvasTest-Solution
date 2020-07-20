using DrawingTool.MVC.Contracts.Invokers;
using DrawingTool.MVC.Contracts.Services;
using DrawingTool.MVC.DataTransferObjects;
using DrawingTool.MVC.ViewModels;

namespace DrawingTool.MVC.Services
{
    public class DrawService : IDrawService
    {
        private readonly IGenerateDrawInvoker generateDrawInvoker;

        public DrawService(IGenerateDrawInvoker generateDrawInvoker)
        {
            this.generateDrawInvoker = generateDrawInvoker;
        }

        public DrawResultDTO GenerateDrawFromCommands(DrawViewModel drawViewModel)
        {
            return this.generateDrawInvoker.Execute(drawViewModel);
        }
    }
}