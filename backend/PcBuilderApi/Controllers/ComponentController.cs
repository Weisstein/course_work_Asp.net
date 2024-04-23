using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcBuilderApi.Data;
using PcBuilderApi.Dtos;

namespace PcBuilderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ComponentController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<ComponentGet>> GetAll()
        {
            var components = await _dataContext.components
                .AsNoTracking()
                .ToListAsync();

            var response = components.Select(c => new ComponentGet(c.Id,c.Name,c.Description,c.Price));
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ComponentGet>> GetById(int id)
        {
            var component = await _dataContext.components
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (component == null)
            {
                return NotFound();
            }

            var response = new ComponentGet(component.Id, component.Name, component.Description, component.Price);
            return Ok(response);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<ComponentGet>> GetByFilter(string typeName, string value)
        {
            var components = _dataContext.components
                .Include(c => c.Type)
                .Include(c => c.Characts);
                

            if (!string.IsNullOrEmpty(typeName))
            {
                components.Where(c => c.Type.Name.Contains(typeName));
            }

            if (!string.IsNullOrEmpty(value))
            {
                components.Include(c => c.Characts.Where(cc => cc.Value.Contains(value)));
            }

            if (components == null)
            {
                return NotFound();
            }

           await components.AsNoTracking()
                .ToListAsync();

            var response = components.Select(c => new ComponentGet(c.Id, c.Name, c.Description, c.Price));
            return Ok(response);
        }
    }
}
