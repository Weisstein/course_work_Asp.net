using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcBuilderApi.Data;
using PcBuilderApi.Dtos;
using PcBuilderApi.Models;
using System.Reflection;

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

            var response = components.Select(c => new ComponentGet(c.Id, c.Name, c.Description, c.Price));
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
        public async Task<ActionResult<ComponentGet>> GetByFilter(string? typeName, string? value, decimal min, decimal max)
        {
            var components = from c in _dataContext.components
                             join ct in _dataContext.componentTypes on c.TypeId equals ct.Id
                             join cc in _dataContext.charact on c.Id equals cc.ComponentId
                             where ct.Name.Contains(typeName) || cc.Value.Contains(value) || 
                             (c.Price >= min && c.Price <= max)
                             select new
                             {
                                 c.Id,
                                 c.Name,
                                 c.Description,
                                 c.Price
                             };               

            await components
                .AsNoTracking()
                .ToListAsync();

            if (!components.Any())
            {
                return NotFound();
            }

            var response = components.Select(c => new ComponentGet(c.Id, c.Name, c.Description, c.Price));                        
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Add(ComponentPost request)
        {
            var newComponent = new Component
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                TypeId = request.TypeId
            };

            var caract = request.Characts.Select(cc => new ComponentCharact { Name = cc.Name, Value = cc.Value, Component = newComponent}).ToList();
            
            newComponent.Characts = caract;

            _dataContext.components.Add(newComponent);
            await _dataContext.SaveChangesAsync();


            return Ok(newComponent.Id);
        }
    }
}
