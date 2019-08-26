using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIWeb.Models;

namespace APIWeb.Controllers
{
    [Route("api/hardware")]
    [ApiController]
    public class hardwareController : ControllerBase
    {
        private readonly context _context;

        public hardwareController(context context)
        {
            _context = context;

            if (_context.hardware.Count() == 0)
            {
                // Create a new user if collection is empty,
                // which means you can't delete all users.
                _context.hardware.Add(new hardware {  HardwareName = "prueba2" });
                _context.SaveChanges();
            }
        }
        // GET: api/hardware
        [HttpGet]
        public async Task<ActionResult<IEnumerable<hardware>>> Gethardwares()
        {
            return await _context.hardware.ToListAsync();
        }

        // GET: api/hardware/5
        [HttpGet("{id}")]
        public async Task<ActionResult<hardware>> Gethardware(long id)
        {
            var hardware = await _context.hardware.FindAsync(id);

            if (hardware == null)
            {
                return NotFound();
            }

            return hardware;
        }
        // POST: api/hardware
        [HttpPost]
        public async Task<ActionResult<hardware>> Posthardware(hardware u)
        {
            _context.hardware.Add(u);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Gethardware), new { id = u.Id }, u);
        }
        // PUT: api/hardware/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Puthardware(long id, hardware item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE: api/hardware/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletehardware(long id)
        {
            var hardware = await _context.hardware.FindAsync(id);

            if (hardware == null)
            {
                return NotFound();
            }

            _context.hardware.Remove(hardware);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}

