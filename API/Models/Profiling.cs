using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_r_profilings")]
public class Profiling
{
    [Key, Column("nik"), MaxLength(20)]
    public string NIK { get; set; }
    [Required, Column("education_id")]
    public int EducationId { get; set; }

    // Relation
    [ForeignKey("EducationId")]
    public Education? Education { get; set; }
    public Account? Account { get; set; }
}
