﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PcBuilderApi.Data;
using PcBuilderApi.Dtos;
using PcBuilderApi.Models;

namespace PcBuilderApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ComponentCharactController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public ComponentCharactController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<ComponentCharactGet>> GetByComponent(int id)
        {
            var charact = await _dataContext.charact
                .Where(c => c.ComponentId == id)
                .AsNoTracking()
                .ToListAsync();

            if (charact == null)
            {
                return NotFound();
            }

            var response = charact.Select(cc => new ComponentCharactGet(cc.Id, cc.Name, cc.Value));
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> Update(int id, ComponentCharactPostPut request)
        {
            var charact = await _dataContext.charact
                .AsNoTracking()
                .FirstOrDefaultAsync(cc => cc.Id == id);

            if (charact == null)
            {
                return NotFound();
            }

            _dataContext.charact
                .ExecuteUpdate(cc => cc
                .SetProperty(cc => cc.Name, request.Name)
                .SetProperty(cc => cc.Value, request.Value)
                );
            await _dataContext.SaveChangesAsync();

            return Ok(id);
        }

        [HttpPost("{componentId:int}")]
        public async Task<ActionResult> Add(int componentId, ComponentCharactPostPut request)
        {
            var componentCharact = new ComponentCharact
            {
                Name = request.Name,
                Value = request.Value,
                ComponentId = componentId
            };

            _dataContext.charact.Add(componentCharact);
            await _dataContext.SaveChangesAsync();
            return Ok(componentCharact.Id);
        }
    }
}
