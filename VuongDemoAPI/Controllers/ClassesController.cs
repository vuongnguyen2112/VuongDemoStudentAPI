using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VuongDemoAPI.Models;

namespace VuongDemoAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ClassesController : ControllerBase
  {
    private readonly DataContext _context;

    public ClassesController(DataContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
    {
      if (_context.Classes == null)
      {
        return NotFound();
      }
      return await _context.Classes.ToListAsync();
    }

    [HttpGet("students/{id}")]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudentsOfClass(int id)
    {
      if (_context.Classes == null)
      {
        return NotFound();
      }
      return await _context.Classes.SelectMany(x => x.Students).Where(x => x.ClassID == id).ToListAsync();
    }

    // GET: api/Classes/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Class>> GetClass(int id)
    {
      if (_context.Classes == null)
      {
        return NotFound();
      }
      var @class = await _context.Classes.FindAsync(id);

      if (@class == null)
      {
        return NotFound();
      }

      return @class;
    }

    // PUT: api/Classes/1
    [HttpPut("{id}")]
    public async Task<IActionResult> PutClass(int id, string className)
    {
      var existClass = await _context.Classes.FindAsync(id);

      if (existClass == null)
        return NotFound();

      existClass.ClassName = className;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ClassExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Classes
    [HttpPost]
    public async Task<ActionResult<Class>> PostClass(Class @class)
    {
      if (_context.Classes == null)
      {
        return Problem("Entity set 'ApplicationDBContext.Classes'  is null.");
      }
      _context.Classes.Add(@class);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetClass", new { id = @class.ClassId }, @class);
    }

    // DELETE: api/Classes/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClass(int id)
    {
      if (_context.Classes == null)
      {
        return NotFound();
      }
      var @class = await _context.Classes.FindAsync(id);
      if (@class == null)
      {
        return NotFound();
      }

      _context.Classes.Remove(@class);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool ClassExists(int id)
    {
      return (_context.Classes?.Any(e => e.ClassId == id)).GetValueOrDefault();
    }
  }
}
