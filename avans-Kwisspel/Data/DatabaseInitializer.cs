using avans_Kwisspel.Model;
using System.Data.Entity;

namespace avans_Kwisspel.Data
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext databaseContext)
        {
            // Category
            Category sports = new Category();
            sports.Text = "Sport";

            Category it = new Category();
            it.Text = "IT";

            Category rekenen = new Category();
            rekenen.Text = "Rekenen";

            databaseContext.Categories.Add(sports);
            databaseContext.Categories.Add(it);
            databaseContext.Categories.Add(rekenen);

            databaseContext.SaveChanges();

            // Quiz
            Quiz quiz = new Quiz();

            databaseContext.Quizzes.Add(new Quiz()
            {
                Text = "Haha"
            });

            databaseContext.Quizzes.Add(quiz);

            databaseContext.SaveChanges();

            // Question
            Question question = new Question();
            question.Text = "Wat gaat er mis?";
            question.Quiz = quiz;
            question.Category = it;

            databaseContext.Questions.Add(question);
   
            databaseContext.SaveChanges();

            // Answer
            Answer a = new Answer();
            a.Text = "Niks";
            a.Question = question;

            Answer b = new Answer();
            b.Text = "Alles";
            b.Question = question;

            Answer c = new Answer();
            c.Text = "Je moeder";
            c.Question = question;
            c.isCorrect = true;

            databaseContext.Answers.Add(a);
            databaseContext.Answers.Add(b);
            databaseContext.Answers.Add(c);

            databaseContext.SaveChanges();
        }
    }
}
