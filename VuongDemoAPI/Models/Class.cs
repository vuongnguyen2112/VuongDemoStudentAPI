namespace VuongDemoAPI.Models
{
  public class Class
  {
    public int ClassId { get; set; }
    public string ClassName { get; set; } = string.Empty;
    public List<Student> Students { get; set; }
  }
}
