using avans_Kwisspel.Model;
using System.Data.Entity;

namespace avans_Kwisspel.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() :
            base("name=KwisspelLocalDb")
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }        
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
}
