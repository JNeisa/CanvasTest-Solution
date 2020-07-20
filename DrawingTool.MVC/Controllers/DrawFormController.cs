using DrawingTool.MVC.Contracts.Services;
using DrawingTool.MVC.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace DrawingTool.MVC.Controllers
{
    [RoutePrefix("generate-draw")]
    public class DrawFormController : Controller
    {
        private readonly IDrawService drawService;

        public DrawFormController(IDrawService drawService)
        {
            this.drawService = drawService;
        }

        [Route(Name = "Draw Form")]
        public ActionResult Index()
        {
            return View(new DrawViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("generate", Name ="Generate Draw")]
        public ActionResult GenerateDraw(DrawViewModel drawFormViewModel)
        {
            if (ModelState.IsValid)
            {
                var file = this.drawService.GenerateDrawFromCommands(drawFormViewModel);

                if (file.Errors != null && file.Errors.Any())
                {
                    var errors = string.Join("<br>", file.Errors);
                    ModelState.AddModelError("File Validation", errors);

                    return View("Index", drawFormViewModel);
                }

                if (file.FileContent != null)
                {
                    return File(file.FileContent, "text/plain", "output.txt");
                }
            }

            return View("Index", drawFormViewModel);
        }
    }
}