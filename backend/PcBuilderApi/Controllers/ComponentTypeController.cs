using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcBuilderApi.Data;
using PcBuilderApi.Dtos;
using PcBuilderApi.Models;

namespace PcBuilderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentTypeController : ControllerBase
    {
        private readonly DataContext _dataContext;
        
        public ComponentTypeController(DataContext dataContext) {  _dataContext = dataContext; }

        [HttpGet]
        public async Task<ActionResult<ComponentTypeGet>> GetAll()
        {
            var componentType = await _dataContext.componentTypes
                .AsNoTracking()
                .ToListAsync();

            var response = componentType.Select(ct => new ComponentTypeGet(ct.Id, ct.Name));

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ComponentTypeGet>> GetById(int id)
        {
            var componentType = await _dataContext.componentTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(ct => ct.Id == id);
                
            if (componentType == null)
            {
                return NotFound();
            }

            var response = new ComponentTypeGet(componentType.Id, componentType.Name);
            return Ok(response);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<ComponentTypeGet>> GetById(string name)
        {
            var componentType = await _dataContext.componentTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(ct => ct.Name.Contains(name));

            if (componentType == null)
            {
                return NotFound();
            }

            var response = new ComponentTypeGet(componentType.Id, componentType.Name);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ComponentTypePostPut request)
        {
            var newType = new ComponentType
            {
                Name = request.Name
            };

            if (string.IsNullOrEmpty(request.Name))
            {
                return BadRequest();
            }

            _dataContext.componentTypes.Add(newType);
            await _dataContext.SaveChangesAsync();


            return Ok(newType.Id);
        }

        [HttpPut]
        public async Task<ActionResult> Update(int id, ComponentTypePostPut request)
        {
            var oldType = await _dataContext.componentTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(ct => ct.Id == id);

            if (oldType == null) 
            {
                return NotFound();            
            }

            await _dataContext.componentTypes
                .Where(ct => ct.Id == id)
                .ExecuteUpdateAsync(ct => ct
                .SetProperty(ct => ct.Name, request.Name)
                );
            await _dataContext.SaveChangesAsync();

            return Ok(id);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var deletedType = await _dataContext.componentTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(ct => ct.Id == id);

            if (deletedType == null)
            {
                return NotFound();
            }

            await _dataContext.componentTypes
                .Where(ct => ct.Id == id)
                .ExecuteDeleteAsync();
            await _dataContext.SaveChangesAsync();
            return Ok(id);
        }
    }
}
