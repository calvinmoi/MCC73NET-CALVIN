using System.ComponentModel.DataAnnotations;

namespace API.Models;

[Table("tb_m_universities")]
public class University
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Required, Column("name"), MaxLength(20)]
    public string Name { get; set; }

    // Relation
    public ICollection<Education>? Educations { get; set; }
}
