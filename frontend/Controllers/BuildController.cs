using Microsoft.AspNetCore.Mvc;

namespace frontend.Controllers
{
    public class BuildController : Controller
    {
        public async Task<IActionResult> Index(int id)
        {

            return View();
        }

        public async Task<IActionResult> Create()
        {

            return View();
        }
    }
}
