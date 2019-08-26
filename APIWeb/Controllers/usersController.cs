using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIWeb.Models;
using Microsoft.AspNetCore.Cors;

namespace APIWeb.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly context _context;

        public usersController(context context)
        {
            _context = context;

            if (_context.users.Count() == 0)
            {
                // Create a new user if collection is empty,
                // which means you can't delete all users.
                _context.users.Add(new user { UserName="jesus92", Name = "jesus", LastName = "enrique", Age = 27 });
                _context.SaveChanges();
            }
        }
        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<user>>> Getusers()
        {
            return await _context.users.ToListAsync();
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<user>> Getusers(long id)
        {
            var user = await _context.users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        // POST: api/users
        //[EnableCors("AnotherPolicy")]
        [HttpPost]
        public async Task<ActionResult<user>> Postusers(user u)
        {
            _context.users.Add(u);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Getusers), new { id = u.Id }, u);
        }
        // PUT: api/softwares/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putuser(long id, user item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE: api/softwares/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteuser(long id)
        {
            var user = await _context.users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}

