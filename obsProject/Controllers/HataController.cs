using Microsoft.AspNetCore.Mvc;

namespace obsProject.Controllers
{
    public class HataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
