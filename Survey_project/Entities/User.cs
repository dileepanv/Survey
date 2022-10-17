using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey_project.Entities
{
    [Table("user")]
    public class User
    {

        public User()
        {
            UserRole = new HashSet<UserRole>();
            RefreshToken = new HashSet<RefreshToken>();
            Verification = new HashSet<Verification>();
           
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [StringLength(25)]
        public string  Name { get; set; }

        [Column("email")]
        [StringLength(50)]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("is_verified")]
        public bool IsVerified { get; set; }




        [InverseProperty("User")]
        public virtual ICollection<UserRole> UserRole { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<RefreshToken> RefreshToken { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Verification> Verification { get; set; }


    }
}
