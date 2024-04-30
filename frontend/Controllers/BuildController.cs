using frontend.Models;
using frontend.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace frontend.Controllers
{
    public class BuildController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        Uri baseAddress = new Uri("http://localhost:5088/api/");

        public BuildController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        public async Task<IActionResult> Index(int id)
        {

            return View();
        }



        [HttpGet]
        public ViewResult AddBuild()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBuild(Build build)
        {
            List<int> componentsIds = new List<int>();
            foreach (var item in build.Components)
            {
                componentsIds.Add(item.Id);
            }

            if (ModelState.IsValid)
            {
                BuildPostPut post = new BuildPostPut(
                build.Name,
                build.Description,
                componentsIds
                );
                StringContent content = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");
                await _httpClient.PostAsync(baseAddress + "Build/Add", content);
            }
            return View();
        }
    }
}
