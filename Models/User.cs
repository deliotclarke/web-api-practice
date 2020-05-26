
namespace my_web_api.Models
{
  public class User
  {
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    private string Password { get; set; }
  }
}