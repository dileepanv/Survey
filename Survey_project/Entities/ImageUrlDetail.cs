using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey_project.Entities
{
    [Table("image_url_detail")]
    public class ImageUrlDetail
    {

        [Key]
        [Column("image_url_id")]
        public int ImageUrlId { get; set; }

        [Column("base_url")]
        public string BaseUrl { get; set; }

        [Column("image_url")]
        public string ImageUrl { get; set; }

        [Column("question_id")]
        public int QuestionId { get; set; }

        [Column("survey_id")]
        public int SurveyId { get; set; }

        [Column("question_type_id")]
        public int QuestionTypeId { get; set; }

        [ForeignKey(nameof(QuestionTypeId))]
        [InverseProperty("ImageUrlDetail")]

        public QuestionType QuestionType { get; set; }

        [ForeignKey(nameof(QuestionId))]
        [InverseProperty("ImageUrlDetail")]

        public Question Question { get; set; }

        [ForeignKey(nameof(SurveyId))]
        [InverseProperty("ImageUrlDetail")]

        public Survey Survey { get; set; }
    }
}
