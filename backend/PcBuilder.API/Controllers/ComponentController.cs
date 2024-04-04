using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PcBuilder.API.Contracts;
using PcBuilder.Core.Abstractions;
using PcBuilder.Core.Models;

namespace PcBuilder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentController : ControllerBase
    {
        private readonly IComponentServise _componentServise;
        public ComponentController(IComponentServise componentServise)
        {
            _componentServise = componentServise;
        }


        [HttpGet]
        public async Task<ActionResult<List<ComponentResponse>>> GetAll()
        {
            var components = await _componentServise.GetAll();

            var response = components.Select(c => new ComponentResponse(c.Id, c.Title, c.Description, c.Price, c.TypeID));

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ComponentResponse>> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("компонент не найден");
            }

            var components = await _componentServise.GetById(id);

            var response = new ComponentResponse(components.Id, components.Title, components.Description, components.Price, components.TypeID);
            return Ok(response);
        }

        [HttpGet("{typeid}/{name}/{charid}/{value}")]
        public async Task<ActionResult<List<ComponentResponse>>> GetByFilter(Guid? typeId, string? name, Guid? charid, string? value)
        {
            var components = await _componentServise.GetByFilter(typeId, name, charid, value);

            var response = components.Select(c => new ComponentResponse(c.Id, c.Title, c.Description, c.Price, c.TypeID));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Add([FromBody] ComponentRequest request)
        {
            var (component, error) = Component.Create(
                Guid.NewGuid(),
                request.Title,
                request.Description,
                request.Price,
                request.TyptId
                );

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            await _componentServise.Add(component);
            return Ok();
        }

        [HttpPut("{id:guid}/{title}/{description}/[?]{price:decimal}")]
        public async Task<ActionResult<Guid>> Update(Guid id, [FromBody] ComponentRequestPut request)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("компонент не найден");
            }

            var component = _componentServise.Update(id, request.Title, request.Description, request.Price);

            return Ok(component);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("компонент не найден");
            }

            var component = _componentServise.Delete(id);

            return Ok(component);
        }
    }
}
