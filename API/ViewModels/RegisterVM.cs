using API.Models;
namespace API.ViewModels;

public class RegisterVM
{
  public string UniversityName { get; set; }
  public string GPA { get; set; }
  public string Degree { get; set; }
  public string FullName { get; set; }
  public string Phone { get; set; }
  public Gender Gender { get; set; }
  public DateTime BirthDate { get; set; }
  public int Salary { get; set; }
  public string Email { get; set; }
  public string Password { get; set; }
}
