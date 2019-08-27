using APIWeb.Data;
using APIWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologyController : ControllerBase
    {
        private readonly TechnologyRepository _repository;

        public TechnologyController(TechnologyRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Technology>>> Get()
        {
            return await _repository.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Technology>> Get(long id)
        {
            var response = await _repository.GetById(id);
            if (response == null) { return NotFound(); }
            return response;
        }
        [HttpPost]
        public async Task Post([FromBody] Technology technology)
        {
            await _repository.Insert(technology);
        }
        [HttpPut("{id}")]
        public async Task PutAsync(long Id, [FromBody] Technology technology)
        {
            await _repository.Update(Id, technology);
        }
        [HttpDelete("{id}")]
        public async Task Delete(long id)
        {
            await _repository.DeleteById(id);
        }

    }
}