using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey_project.Entities
{
    [Table("refresh_token")]
    public class RefreshToken
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("token")]
        public string Token { get; set; }

        [Column("expires")]
        public DateTime Expires { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }

        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("RefreshToken")]
        public virtual User User { get; set; }
    }
}
