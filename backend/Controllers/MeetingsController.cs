using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    public class MeetingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
