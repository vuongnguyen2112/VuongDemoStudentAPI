﻿namespace VuongDemoAPI.Models
{
  public class User
  {
    public int Id { get; set; }
    public string UserName { get; set; }
    public byte[] Password{ get; set; }
    public byte[] Salt { get; set; }
  }
}
