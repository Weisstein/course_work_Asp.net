using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly HttpClient _httpClient;
        Uri baseAddress = new Uri("http://localhost:5088/api/");

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }

        public async Task<IActionResult> Index()
        {
            List<Build> builds = new List<Build>();
            using (var response = await _httpClient.GetAsync(baseAddress + "Build/GetAll"))
            {
                string data = response.Content.ReadAsStringAsync().Result;
                builds = JsonConvert.DeserializeObject<List<Build>>(data);
            }
            return View(builds);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
