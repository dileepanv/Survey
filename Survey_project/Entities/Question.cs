using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey_project.Entities
{
    [Table("question")]
    public class Question
    {
        public Question()
        {
            ImageUrlDetail = new HashSet<ImageUrlDetail>();
            Options = new HashSet<Options>();
            UserAnswer = new HashSet<UserAnswer>();
        }

        [Key]
        [Column("question_id")]
        public int QuestionId { get; set; }

        [Column("survey_id")]
        public int SurveyId { get; set; }

        [Column("create_date")]
        public DateTime CreateDate { get; set; }

        [Column("question_name")]
        public string QuestionName { get; set; }

        [Column("question_type_id")]
        public int QuestionTypeId { get; set; }

        [InverseProperty("Question")]
        public virtual ICollection<Options> Options { get; set; }

        [InverseProperty("Question")]
        public virtual ICollection<ImageUrlDetail> ImageUrlDetail { get; set; }

        [InverseProperty("Question")]
        public virtual ICollection<UserAnswer> UserAnswer { get; set; }

        [ForeignKey(nameof(SurveyId))]
        [InverseProperty("Question")]
        public Survey Survey { get; set; }

        [ForeignKey(nameof(QuestionTypeId))]
        [InverseProperty("Question")]
        public QuestionType QuestionType { get; set; }

    }
}
