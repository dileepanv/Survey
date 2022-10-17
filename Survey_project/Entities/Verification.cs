using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey_project.Entities
{
    [Table("verification")]
    public class Verification
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("is_otp_verified")]
        public bool IsOtpVerified { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("Verification")]
        public User User { get; set; }

    }
}
