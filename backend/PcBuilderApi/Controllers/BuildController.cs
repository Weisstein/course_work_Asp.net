using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcBuilderApi.Data;
using PcBuilderApi.Dtos;
using PcBuilderApi.Models;
using System.ComponentModel;
using System.Linq;

namespace PcBuilderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public BuildController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<BuildGet>> GetAll()
        {
            var build = await _dataContext.builds
                .Include(b => b.Components)
                .AsNoTracking()
                .ToListAsync();

            var response = build.Select(b => new BuildGet(b.Id, b.Name, b.Description, b.Components));

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BuildGet>> GetById(int id)
        {
            var build = await _dataContext.builds
                .Include(b => b.Components)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            if (build == null)
            {
                return NotFound();
            }

            var response = new BuildGet(build.Id, build.Name, build.Description, build.Components);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] BuildPostPut request)
        {
            var components = await _dataContext.components
                .Where(c => request.componentIds.Contains(c.Id))
                .ToListAsync();

            var build = new Build
            {
                Name = request.Name,
                Description = request.Descriptions
            };

            build.Components = components;

            _dataContext.builds.Add(build);


            await _dataContext.SaveChangesAsync();
            return Ok(build.Id);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] BuildPostPut request)
        {
            var build = await _dataContext.builds
                .AsNoTracking()
                .Include(b => b.Components)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (build == null)
            {
                return NotFound();
            }
            build.Name = request.Name;
            build.Description = request.Descriptions;

            var existingIds = build.Components.Select(c => c.Id).ToList();
            var selectedIds = request.componentIds.ToList();
            var toAdd = selectedIds.Except(existingIds).ToList();
            var toRemove = existingIds.Except(selectedIds).ToList();
            foreach (var aitem in toAdd)
            {
                _dataContext.Database.ExecuteSqlInterpolated($"INSERT INTO BuildComponent (BuildsId,ComponentsId) VALUES ({id},{aitem})");
            } 
            foreach (var ritem in toRemove)
            {
                _dataContext.Database.ExecuteSqlInterpolated($"DELETE FROM BuildComponent WHERE BuildsId = {id} AND ComponentsId = {ritem}");
            }
            _dataContext.Entry(build).State = EntityState.Modified;
            _dataContext.SaveChanges();
            return Ok(build.Id);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var build = await _dataContext.builds
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Id == id);

            if (build == null)
            {
                return NotFound();
            }

            _dataContext.builds
                .Where(b => b.Id == id)
                .ExecuteDelete();
            _dataContext.SaveChanges();
            return Ok(build.Id);
        }
    }
}
