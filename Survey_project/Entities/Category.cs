using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey_project.Entities
{
    [Table("category")]
    public class Category
    {
        public Category ()
        {
            Survey = new HashSet<Survey>();
        }

        [Key]
        [Column("category_id")]
        public int CategoryId { get; set; }

        [Column("category_name")]
        public string CategoryName { get; set; }

        [InverseProperty ("Category")]
        public virtual ICollection <Survey> Survey { get; set; }


    }
}
