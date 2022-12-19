using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_educations")]
public class Education
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Required, Column("degree"), MaxLength(50)]
    public string Degree { get; set; }
    [Required, Column("gpa"), MaxLength(5)]
    public string GPA { get; set; }
    [Required, Column("university_id")]
    public int UniversityId { get; set; }

    // Relation
    [ForeignKey("UniversityId")]
    public University? University { get; set; }
    public Profiling? Profiling { get; set; }
}
