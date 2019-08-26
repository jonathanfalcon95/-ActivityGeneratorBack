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
    public class assignmentsController : ControllerBase
    {
        private readonly AssignmentsRepository _repository;

        public assignmentsController(AssignmentsRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        // GET: api/users
        /*[HttpGet]
        public async Task<ActionResult<IEnumerable<assignment>>> Getassignment(int userid)
        {
            // await _context.assignment.Where(x => x.UserId == 1);
            var UserId = await _context.assignment.Where(x => x.UserId == userid).ToListAsync();

            return UserId;
        }*/

        // GET api/values
        /* [HttpGet]
         public async Task<ActionResult<IEnumerable<Assignments>>> Get()
         {
             return await _repository.GetAll();
         }*/

        // GET api/values/5
        [HttpGet("users/{id}")]
        public async Task<ActionResult<object>> Get(int id)
        {
            var response = await _repository.GetById(id);
            if (response == null) { return NotFound(); }
            return response;
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] Assignments assignments)
        {
            await _repository.Insert(assignments);
        }

        // PUT api/values/5
        /*[HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }*/

        //DELETE api/values/5
        [HttpPost("users/{id}")]
        public async Task Delete([FromBody] Assignments assignments)
        {
            await _repository.DeleteById(assignments);
        }
    }
}

