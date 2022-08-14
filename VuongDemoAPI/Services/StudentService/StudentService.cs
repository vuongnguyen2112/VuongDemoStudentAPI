using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VuongDemoAPI.Models;
using VuongDemoAPI.Services.StudentService;
using VuongDemoAPI.DTO;

namespace VuongDemoAPI.Controllers
{

  public class StudentService : IStudentService
  {
    //private static List<Student> students = new List<Student>
    //  {
    //    new Student {Id = 2, Name = "Giang", ClassID = 2, Grade = 8}
    //  };
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public StudentService(DataContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<Response<List<GetStudentDTO>>> GetStudents()
    {
      var response = new Response<List<GetStudentDTO>>();
      try
      {
        var dBStudents = await _context
          .Students
          .Select(s => _mapper.Map<GetStudentDTO>(s))
          .ToListAsync();
        response.Data = dBStudents;
      }
      catch (Exception ex)
      {
        response.Success = false;
        response.Message = ex.Message;
      }

      return response;
    }


    public async Task<Response<List<GetStudentDTO>>> GetStudent(int id)
    {
      var response = new Response<List<GetStudentDTO>>();
      try
      {
        var dBStudent = await _context.Students.FindAsync(id);
        if (dBStudent == null)
        {
          throw new Exception("Student is not exist");
        }
        var listStudent = new List<GetStudentDTO>();
        listStudent.Add(_mapper.Map<GetStudentDTO>(dBStudent));
        response.Data = listStudent;
      }
      catch (Exception ex)
      {
        response.Success = false;
        response.Message = ex.Message;
      }

      return response;
      //var student = await _context.Students.FindAsync(id);
      //if (student == null)
      //{
      //  return NotFound("Student Not Found");
      //}
      //return Ok(student);
    }


    public async Task<Response<List<GetStudentDTO>>> PostStudent(AddStudentDTO newStudent)
    {
      var response = new Response<List<GetStudentDTO>>();
      var student = _mapper.Map<Student>(newStudent);
      _context.Students.Add(student);

      await _context.SaveChangesAsync();
      response.Data = await _context
        .Students
        .Select(student => _mapper.Map<GetStudentDTO>(student))
        .ToListAsync();
      return response;
      //_context.Students.Add(student);
      //await _context.SaveChangesAsync();
      //var newStudent = await _context.Students.FindAsync(student.Id);
      //return Ok(newStudent);
    }


    public async Task<Response<List<GetStudentDTO>>> PutStudent(UpdateStudentDTO updatedStudent)
    {
      var response = new Response<List<GetStudentDTO>>();
      try
      {
        var dBStudent = await _context.Students.FirstOrDefaultAsync(s => s.Id == updatedStudent.Id);
        if (dBStudent == null)
        {
          throw new Exception("Student is not exist");
        }
        _mapper.Map(updatedStudent, dBStudent);
        await _context.SaveChangesAsync();
        var listStudent = new List<GetStudentDTO>();
        listStudent.Add(_mapper.Map<GetStudentDTO>(dBStudent));
        response.Data = listStudent;
      }
      catch (Exception ex)
      {
        response.Success = false;
        response.Message = ex.Message;
      }
      return response;
    }

    public async Task<Response<List<GetStudentDTO>>> DeleteStudent(int id)
    {
      var response = new Response<List<GetStudentDTO>>();
      try
      {
        var deleteStudent = await _context.Students.FindAsync(id);
        if (deleteStudent == null)
        {
          throw new Exception("Student is not exist");
        }
        _context.Students.Remove(deleteStudent);
        await _context.SaveChangesAsync();

        var students = await _context
          .Students
          .Select(s => _mapper.Map<GetStudentDTO>(s))
          .ToListAsync();
        response.Data = students;
      }
      catch (Exception ex)
      {
        response.Success = false;
        response.Message = ex.Message;
      }
      return response;
    }
  }
}
