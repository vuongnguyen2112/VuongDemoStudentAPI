using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VuongDemoAPI.Models;
using VuongDemoAPI.Services.StudentService;
using VuongDemoAPI.DTO;

namespace VuongDemoAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StudentsController : ControllerBase
  {
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
      _studentService = studentService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<GetStudentDTO>>>> GetStudents()
    {
      return Ok(await _studentService.GetStudents());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Response<GetStudentDTO>>> GetStudent(int id)
    {
      return Ok(await _studentService.GetStudent(id));
    }

    [HttpPost]
    public async Task<ActionResult<Response<List<GetStudentDTO>>>> PostStudent(AddStudentDTO student)
    {
      var response = await _studentService.PostStudent(student);
      return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<List<Student>>> PutStudent(UpdateStudentDTO student)
    {
      var response = await _studentService.PutStudent(student);
      if (response.Data == null)
      {
        return NotFound(response);
      }
      return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<Student>>> DeleteStudent(int id)
    {
      var response = await _studentService.DeleteStudent(id);
      if (response.Data == null)
      {
        return NotFound(response);
      }
      return Ok(response);
    }
  }
}

