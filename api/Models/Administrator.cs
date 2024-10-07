namespace api.Models
{
  public class Administrator
  {
    public int ID { get; set; }
    public required string UserName { get; set; }
    public required string PassWord { get; set; }
    public required string FullName { get; set; }
    public required string Phone { get; set; }
    public required string UserCreated { get; set; }
    public required string UserUpdated { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
  }
}