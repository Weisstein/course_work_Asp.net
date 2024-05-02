using frontend.Models;
using frontend.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;
using System.Xml.Linq;

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

        private async void GetTypes()
        {
            List<ComponentType> types = new List<ComponentType>();
            using (var response = await _httpClient.GetAsync(baseAddress + "ComponentType/GetAll"))
            {
                string data = response.Content.ReadAsStringAsync().Result;
                types = JsonConvert.DeserializeObject<List<ComponentType>>(data);
            }
            ViewBag.Types = types;
        }

        private async void AddCompoent(int id)
        {
            Component component = null;
            using (var response = await _httpClient.GetAsync(baseAddress + $"Component/GetById/{id}"))
            {
                string data = response.Content.ReadAsStringAsync().Result;
                component = JsonConvert.DeserializeObject<Component>(data);
            }

            ViewBag.Component = component;
        }

        public async Task<IActionResult> AddBuild()
        {
            GetTypes();
            ViewBag.Price = 0;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBuild([Bind("Name", "Description")]Build build)
        {
            List<int> componentsIds = new List<int>();
            if (ViewBag.Component != 0 && ViewBag.Component != null)
                build.Components.Add(ViewBag.Component);
            if (build.Components.Count != 0) 
            { 
                
                foreach (var item in ViewBag.Components)
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
