using Microsoft.AspNetCore.Mvc;
using PcBuilder.API.Contracts;
using PcBuilder.BL.Servises;
using PcBuilder.Core.Models;

namespace PcBuilder.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ComponentTypeController : ControllerBase
    {
        private readonly IComponentTypeServise _componentTypeServise;

        public ComponentTypeController(IComponentTypeServise componentTypeServise)
        {
            _componentTypeServise = componentTypeServise;
        }
        /// <summary>
        /// Get all types
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<ComponentTypeResponse>>> GetAll()
        {
            var types = await _componentTypeServise.GetAll();

            var response = types.Select(t => new ComponentTypeResponse(t.Id, t.Name));

            return Ok(response);
        }

        /// <summary>
        /// Get all types by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ComponentTypeResponse>> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Тип компонента не найден");
            }
            var types = await _componentTypeServise.GetById(id);

            var response = new ComponentTypeResponse(types.Id, types.Name);

            return Ok(response);
        }

        /// <summary>
        /// Create new type
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateType(Guid id, [FromBody] ComponentTypeRequest request)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Тип компонента не найден");
            }

            var type = await _componentTypeServise.Update(id,request.Name);

            return Ok(type);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteType(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Тип компонента не найден");
            }

            var type = await _componentTypeServise.Delete(id);
            return Ok(type);
        }
    }
}
