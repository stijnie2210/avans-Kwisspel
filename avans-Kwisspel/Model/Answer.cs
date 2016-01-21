using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace avans_Kwisspel.Model
{
    [Table("Answer")]
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }

        public int QuestionId { get; set; }
        [Required, ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
    }
}
