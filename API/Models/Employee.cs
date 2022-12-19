using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_employees")]
public class Employee
{
    [Key, Column("nik"), MaxLength(20)]
    public string NIK { get; set; }
    [Required, Column("first_name"), MaxLength(20)]
    public string FirstName { get; set; }
    [Column("last_name"), MaxLength(20)]
    public string LastName { get; set; }
    [Required, Column("phone_number"), MaxLength(15)]
    public string Phone { get; set; }
    [Required, Column("birth_date")]
    public DateTime BirthDate { get; set; }
    [Required, Column("salary")]
    public int Salary { get; set; }
    [Required, Column("email"), MaxLength(50)]
    public string Email { get; set; }
    [Required, Column("gender"), MaxLength(15)]
    public Gender Gender { get; set; }

    // Relation
    public Account? Account { get; set; }
}

public enum Gender
{
    Male,
    Female
}
