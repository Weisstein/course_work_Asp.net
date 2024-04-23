﻿using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcBuilderApi.Data;
using PcBuilderApi.Dtos;
using PcBuilderApi.Models;
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
        public async Task<ActionResult> Add([FromBody] BuildPost request)
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

        [HttpPut]
        public async Task<ActionResult> Update(int id)
        {

        }
    }
}
