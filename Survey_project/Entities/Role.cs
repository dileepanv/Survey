using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey_project.Entities
{

    [Table("role")]
    public class Role
    {
        public Role()
        {
            UserRole = new HashSet<UserRole>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [StringLength(20)]
        public string Name { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<UserRole> UserRole { get; set; }

    }
}
