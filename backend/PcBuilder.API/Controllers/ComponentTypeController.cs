using Microsoft.AspNetCore.Mvc;
using PcBuilder.API.Contracts;
using PcBuilder.BL.Servises;
using PcBuilder.Core.Models;

namespace PcBuilder.API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ComponentTypeController : ControllerBase
    {
        private readonly IComponentTypeServise _componentTypeServise;

        public ComponentTypeController(IComponentTypeServise componentTypeServise)
        {
            _componentTypeServise = componentTypeServise;
        }

        [HttpGet]
        public async Task<ActionResult<List<ComponentTypeResponse>>> GetTypes()
        {
            var types = await _componentTypeServise.GetAllComponentTypes();

            var response = types.Select(t => new ComponentTypeResponse(t.Id,t.Name));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddType([FromBody] ComponentTypeRequest request)
        {
            var (type, error) = ComponentType.Create(
                Guid.NewGuid(),
                request.Name
                );
        }
    }
}
