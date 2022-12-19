using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_r_accounts_roles")]
public class AccountRole
{
    [Key, Column("id")]
    public int Id { get; set; }
    [Required, Column("account_nik"), MaxLength(20)]
    public string AccountNIK { get; set; }
    [Required, Column("role_id")]
    public int RoleId { get; set; }

    // Relation
    [ForeignKey("AccountNIK")]
    public Account? Account { get; set; }
    [ForeignKey("RoleId")]
    public Role? Role { get; set; }
}
