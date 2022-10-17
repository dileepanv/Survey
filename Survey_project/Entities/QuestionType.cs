using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey_project.Entities
{
    [Table("question_type")]
    public class QuestionType
    {
        public QuestionType()
        {
            Options = new HashSet<Options>();
            ImageUrlDetail = new HashSet<ImageUrlDetail>();
            Question = new HashSet<Question>();
        }

        [Key]
        [Column("question_type_id")]
        public int QuestionTypeId { get; set; }

        [Column("question_type_name")]
        public string QuestionTypeName { get; set; }

        [InverseProperty("QuestionType")]
        public virtual ICollection<Options> Options { get; set; }

        [InverseProperty("QuestionType")]
        public virtual ICollection<ImageUrlDetail> ImageUrlDetail { get; set; }

        [InverseProperty("QuestionType")]
        public virtual ICollection<Question> Question { get; set; }

    }
}
