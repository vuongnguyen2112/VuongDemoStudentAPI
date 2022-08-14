namespace VuongDemoAPI.DTO
{
  public class GetStudentDTO
  {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ClassID { get; set; }
    public double Grade { get; set; }
  }
}
