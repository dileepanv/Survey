using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey_project.Entities
{
    [Table("options")]
    public class Options
    {
        public Options ()
        {
            UserAnswer = new HashSet<UserAnswer>();
        }

        [Key]
        [Column("option_id")]
        public int OptionId { get; set; }

        [Column("question_type_id")]
        public int QuestionTypeId { get; set; }

        [Column("question_id")]
        public int QuestionId { get; set; }

        [Column("option_name")]
        public string OptionName { get; set; }

        [ForeignKey(nameof(QuestionTypeId))]
        [InverseProperty("Options")]
        public QuestionType QuestionType { get; set; }


        [ForeignKey(nameof(QuestionId))]
        [InverseProperty("Options")]
        public Question Question { get; set; }

        [InverseProperty("Options")]
        public virtual ICollection<UserAnswer> UserAnswer { get; set; }

    }
}
