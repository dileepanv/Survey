using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey_project.Entities
{
    [Table("survey")]
    public class Survey
    {
        public Survey()
        {
            Question = new HashSet<Question>();
            ImageUrlDetail = new HashSet<ImageUrlDetail>();
        }

        [Key]
        [Column("survey_id")]
        public int SurveyId { get; set; }

        [Column("survey_name")]
        public string SurveyName { get; set; }

        [Column("create_date")]
        public DateTime CreateDate { get; set; }

        [Column("tittle")]
        public string Tittle { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("category_id")]
        public int CategoryId { get; set; }

        [Column("isPopular")]
        public bool Ispopular { get; set; }

        [ForeignKey (nameof (CategoryId))]
        [InverseProperty("Survey")]
        public Category Category { get; set; }

        [InverseProperty("Survey")]
        public virtual ICollection<Question> Question { get; set; }

        [InverseProperty("Survey")]
        public virtual ICollection<ImageUrlDetail> ImageUrlDetail { get; set; }

    }

}
