using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey_project.Entities
{

    [Table("password_reset_token")]
    public class PasswordResetToken
    {

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("email")]
        [StringLength(255)]
        public string Email { get; set; }

        [Column("token")]
        [StringLength(255)]
        public string Token { get; set; }

        [Column("expiry_date")]
        public DateTime? ExpiryDate { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }
    }
}
