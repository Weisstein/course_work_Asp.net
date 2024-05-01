using frontend.Models;
using frontend.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        
        public async Task<IActionResult> AddBuild()
        {
            List<Component> components = new List<Component>();
            using (var response = await _httpClient.GetAsync(baseAddress + "Component/GetByFilter?typeName=Видеокарта"))
            {
                string data = response.Content.ReadAsStringAsync().Result;
                components = JsonConvert.DeserializeObject<List<Component>>(data);
            }
            ViewBag.Components = new SelectList(components,"Id","Name",components);
            ViewBag.Price = 0;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBuild(Build build)
        {
            List<int> componentsIds = new List<int>();
            if (build.Components.Count != 0) 
            { 
                
                foreach (var item in build.Components)
                {
                    componentsIds.Add(item.Id);
                    ViewBag.Price += item.Price; 
                }
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
                return RedirectToAction("Index","Home");
            }
            return View();
        }
    }
}
