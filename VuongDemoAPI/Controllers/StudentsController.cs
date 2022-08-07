using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VuongDemoAPI.Models;

namespace VuongDemoAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StudentsController : ControllerBase
  {
    private static List<Student> students = new List<Student>
      {
        new Student {Id = 2, Name = "Giang", ClassID = 2, Grade = 8}
      };
    private readonly DataContext _context;

    public StudentsController(DataContext context)
    {
      _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Student>>> GetStudents()
    {
      return Ok(await _context.Students.ToListAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
      var student = await _context.Students.FindAsync(id);
      if (student == null)
      {
        return NotFound("Student Not Found");
      }
      return Ok(student);
    }

    [HttpPost]
    public async Task<ActionResult<List<Student>>> PostStudent(Student student)
    {
      _context.Students.Add(student);
      await _context.SaveChangesAsync();
      var newStudent = await _context.Students.FindAsync(student.Id);
      return Ok(newStudent);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<List<Student>>> PutStudent(Student request)
    {
      var dBStudent = await _context.Students.FindAsync(request.Id);

      if (dBStudent == null)
      {
        return NotFound("Student Not Found");
      }
      dBStudent.Name = request.Name;
      dBStudent.ClassID = request.ClassID;
      dBStudent.Grade = request.Grade;

      await _context.SaveChangesAsync();
      return Ok(dBStudent);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<Student>>> DeleteStudent(int id)
    {
      var dBStudent = await _context.Students.FindAsync(id);
      if (dBStudent == null)
      {
        return NotFound("Student Not Found");
      }
      _context.Students.Remove(dBStudent);
      _context.SaveChanges();
      return Ok(await _context.Students.ToListAsync());
    }
  }
}
