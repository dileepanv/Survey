using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey_project.Entities
{
    [Table("user_answer")]
    public class UserAnswer
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column ("user_id")]
        public int UserId { get; set; }

        [Column("question_id")]
        public int QuestionId { get; set; }

        [Column("option_id")]
        public int OptionId { get; set; }

        [Column ("create_date")]
        public DateTime CreateDate { get; set; }

        [ForeignKey(nameof(QuestionId))]
        [InverseProperty("UserAnswer")]
        public Question Question { get; set; }


        [ForeignKey(nameof(OptionId))]
        [InverseProperty("UserAnswer")]
        public Options Options{ get; set; }

    }
}
