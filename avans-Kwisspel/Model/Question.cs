using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace avans_Kwisspel.Model
{
    [Table("Question")]
    public partial class Question
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }

        public int QuizId { get; set; }
        [ForeignKey("QuizId")]
        public virtual Quiz Quiz { get; set; }

        public int CategoryId { get; set; }
        [Required, ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual List<Answer> Answers { get; set; }
    }
}
