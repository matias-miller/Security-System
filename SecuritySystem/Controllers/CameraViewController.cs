using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace SecuritySystem.Controllers
{
    public class CameraViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult HandleButtonClick(string mine) {
            Debug.WriteLine("in");
            return View("test");
        }
    }
}
