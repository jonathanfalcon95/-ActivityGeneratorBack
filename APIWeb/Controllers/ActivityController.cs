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
    [Route("api/activity")]
    [ApiController]
    public class ActivityController : ControllerBase
    {


        private readonly ActivityRepository _repository;

        public ActivityController(ActivityRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


        // GET: api/Activity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> Get()
        {
            return await _repository.GetAll();
        }

        // GET: api/Activity/5
        [HttpGet("{activId}")]
        public async Task<ActionResult<object>> Get(long activId)
        {
            var response = await _repository.GetById(activId);
            if (response == null) { return NotFound(); }
            return response;
        }

        // POST api/values
        [HttpPost]
        public async Task Post([Bind("activIdd" )] Activity activity)
        {
            
            await _repository.Insert(activity);
        }

        //// PUT api/values/5
        //[HttpPut("{activId}")]
        //public async Task Put(long activId, [FromBody] object activity)
        //{

        //    await _repository.Update(activId, activity);


        //}


        //// DELETE api/values/5
        //[HttpDelete("{activId}")]
        //public async Task Delete(long activId)
        //{
        //    await _repository.DeleteById(activId);
        //}
    }
}
