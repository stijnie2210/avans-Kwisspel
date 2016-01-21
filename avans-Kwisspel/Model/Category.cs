using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace avans_Kwisspel.Model
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }

        public virtual List<Question> Questions { get; set; }
    }
}
