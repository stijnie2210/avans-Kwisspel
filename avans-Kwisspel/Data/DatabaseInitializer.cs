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
            quiz.Text = "Nog een Coole quiz";

            databaseContext.Quizzes.Add(new Quiz()
            {
                Text = "Coole Quiz"
            });

            databaseContext.Quizzes.Add(quiz);

            databaseContext.SaveChanges();

            // Question
            Question question = new Question();
            question.Text = "Wat is 1+1?";
            question.Quiz = quiz;
            question.Category = rekenen;

            databaseContext.Questions.Add(question);
   
            databaseContext.SaveChanges();

            // Answer
            Answer a = new Answer();
            a.Text = "2";
            a.Question = question;
            a.isCorrect = true;

            Answer b = new Answer();
            b.Text = "Huh";
            b.Question = question;

            Answer c = new Answer();
            c.Text = "Cool";
            c.Question = question;

            databaseContext.Answers.Add(a);
            databaseContext.Answers.Add(b);
            databaseContext.Answers.Add(c);

            databaseContext.SaveChanges();
        }
    }
}
