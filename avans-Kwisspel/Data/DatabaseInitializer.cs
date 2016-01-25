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
            Quiz quiz2 = new Quiz();
            quiz.Text = "Nog een Coole quiz";
            quiz2.Text = "Coole Quiz";

            databaseContext.Quizzes.Add(quiz);
            databaseContext.Quizzes.Add(quiz2);

            databaseContext.SaveChanges();

            // Question
            Question question2 = new Question();
            question2.Text = "Wat is 2+2?";
            question2.Quiz = quiz2;
            question2.Category = rekenen;

            Question question = new Question();
            question.Text = "Wat is 1+1?";
            question.Quiz = quiz;
            question.Category = rekenen;

            databaseContext.Questions.Add(question);
            databaseContext.Questions.Add(question2);
   
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

            Answer a2 = new Answer();
            a2.Text = "2";
            a2.Question = question2;

            Answer b2 = new Answer();
            b2.Text = "4";
            b2.Question = question2;
            b2.isCorrect = true;

            Answer c2 = new Answer();
            c2.Text = "swek";
            c2.Question = question2;

            databaseContext.Answers.Add(a2);
            databaseContext.Answers.Add(b2);
            databaseContext.Answers.Add(c2);
            

            databaseContext.SaveChanges();
        }
    }
}
