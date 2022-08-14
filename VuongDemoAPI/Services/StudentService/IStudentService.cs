using VuongDemoAPI.DTO;
using VuongDemoAPI.Models;

namespace VuongDemoAPI.Services.StudentService
{
  public interface IStudentService
  {
    Task<Response<List<GetStudentDTO>>> GetStudents();
    Task<Response<List<GetStudentDTO>>> GetStudent(int id);
    Task<Response<List<GetStudentDTO>>> PostStudent(AddStudentDTO newStudent);
    Task<Response<List<GetStudentDTO>>> PutStudent(UpdateStudentDTO updatedStudent);
    Task<Response<List<GetStudentDTO>>> DeleteStudent(int id);
  }
}
