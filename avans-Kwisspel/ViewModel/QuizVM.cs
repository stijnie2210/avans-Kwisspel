using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using avans_Kwisspel.Model;

namespace avans_Kwisspel.ViewModel
{
    public class QuizVM : ViewModelBase
    {
        public Quiz _quiz;

        public QuizVM()
        {
            _quiz = new Quiz();
        }

        public QuizVM(Quiz quiz)
        {
            _quiz = quiz;
        }

        public int Id
        {
            get { return _quiz.Id; }
        }

        public string Text
        {
            get { return _quiz.Text; }
            set
            {
                _quiz.Text = value;
                RaisePropertyChanged("Text");
            }
        }

        public virtual List<Question> Questions
        {
            get { return _quiz.Questions ?? (_quiz.Questions = new List<Question>()); }
            set
            {
                _quiz.Questions = value;
                RaisePropertyChanged(() => Questions);
            }
        }

        public int AmountOfQuestions
        {
            get { return Questions.Count; }
        }

        public Quiz toQuiz()
        {
            return _quiz;
        }
    }
}
