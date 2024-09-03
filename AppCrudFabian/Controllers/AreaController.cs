using Microsoft.AspNetCore.Mvc;

namespace AppCrudFabian.Controllers
{
    public class AreaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
