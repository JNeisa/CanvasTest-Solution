using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DrawingTool.MVC.ViewModels
{
    public class DrawViewModel
    {
        [Required]
        public HttpPostedFileBase DrawCommands { get; set; }
    }
}