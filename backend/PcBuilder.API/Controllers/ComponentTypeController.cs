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
        public async Task<ActionResult<List<ComponentTypeResponse>>> GetAll()
        {
            var types = await _componentTypeServise.GetAll();

            var response = types.Select(t => new ComponentTypeResponse(t.Name));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ComponentTypeResponse>> GetById(Guid id)
        {
            var types = await _componentTypeServise.GetById(id);

            var response = new ComponentTypeResponse(types.Name);

            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<Guid>> AddType([FromBody] ComponentTypeRequest request)
        {
            var (type, error) = ComponentType.Create(
                Guid.NewGuid(),
                request.Name
                );
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            await _componentTypeServise.Add(type);

            return Ok();
        }
    }
}
