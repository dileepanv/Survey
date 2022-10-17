using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey_project.Entities
{
    [Table("user_role")]
    public class UserRole
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserRole")]
        public User User { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty("UserRole")]
        public Role Role { get; set; }
    }
}
