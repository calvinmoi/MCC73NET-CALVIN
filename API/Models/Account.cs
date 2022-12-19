using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_accounts")]
public class Account
{
    [Key, Column("nik"), MaxLength(20)]
    public string NIK { get; set; }
    [Required, Column("password"), MaxLength(50)]
    public string Password { get; set; }
    [Column("otp")]
    public int OTP { get; set; }
    [Column("expired_token")]
    public DateTime ExpiredToken { get; set; }
    [Column("is_used")]
    public bool IsUsed { get; set; }

    // Relation
    public Employee? Employee { get; set; }
    public Profiling? Profiling { get; set; }
    public ICollection<AccountRole>? AccountRoles { get; set; }
}
