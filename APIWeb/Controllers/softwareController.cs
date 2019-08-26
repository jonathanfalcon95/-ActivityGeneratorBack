using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIWeb.Models;

namespace APIWeb.Controllers
{
    [Route("api/software")]
    [ApiController]
    public class sotwareController : ControllerBase
    {
        private readonly context _context;

        public sotwareController(context context)
        {
            _context = context;

            if (_context.software.Count() == 0)
            {
                // Create a new user if collection is empty,
                // which means you can't delete all users.
                _context.software.Add(new software { SoftwareName = "prueba1" });
                _context.SaveChanges();
            }
        }
        // GET: api/softwares
        [HttpGet]
        public async Task<ActionResult<IEnumerable<software>>> Getsoftware()
        {
            return await _context.software.ToListAsync();
        }

        // GET: api/softwares/5
        [HttpGet("{id}")]
        public async Task<ActionResult<software>> Getsoftware(long id)
        {
            var software = await _context.software.FindAsync(id);

            if (software == null)
            {
                return NotFound();
            }

            return software;
        }
        // POST: api/software
        [HttpPost]
        public async Task<ActionResult<user>> Postsoftware(software u)
        {
            _context.software.Add(u);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Getsoftware), new { id = u.Id }, u);
        }
        // PUT: api/software/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putsoftware(long id, software item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE: api/software/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletesoftware(long id)
        {
            var software = await _context.software.FindAsync(id);

            if (software == null)
            {
                return NotFound();
            }

            _context.software.Remove(software);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}

