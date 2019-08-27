using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIWeb.Data;
using APIWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevelController : ControllerBase
    {
        private readonly LevelRepository _repository;

        public LevelController(LevelRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] Level level)
        {
            await _repository.Insert(level);
        }
        // GET api/values
        [HttpGet]
         public async Task<ActionResult<IEnumerable<Level>>> Get()
         {
             return await _repository.GetAll();
         }

        // GET api/values/5
        [HttpGet("{levelId}")]
        public async Task<ActionResult<Level>> Get(long levelId)
        {
            var response = await _repository.GetById(levelId);
            if (response == null) { return NotFound(); }
            return response;
        }

        // PUT api/values/5
        [HttpPut("{levelId}")]
        public async Task Put(long levelId, [FromBody] Level level)
        {
            //var response = await _repository.GetById(levelId);
            //if (response == null) { return NotFound(); }
            await _repository.Update(levelId,level);
            //return NoContent();
            
        }


        // DELETE api/values/5
        [HttpDelete("{levelId}")]
        public async Task Delete(long levelId)
        {
            await _repository.DeleteById(levelId);
        }
    }
}
