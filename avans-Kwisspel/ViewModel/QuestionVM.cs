using avans_Kwisspel.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avans_Kwisspel.ViewModel
{
    public class QuestionVM : ViewModelBase
    {
        public Question _question;

        public QuestionVM()
        {
            _question = new Question();
        }

        public QuestionVM(Question question)
        {
            _question = question;
        }

        public int Id
        {
            get { return _question.Id; }
        }

        public string Text
        {
            get { return _question.Text; }
            set
            {
                _question.Text = value;
                RaisePropertyChanged(() => Text);
            }
        }

        public int CategoryId
        {
            get { return _question.CategoryId; }
        }
        public virtual Category Category
        {
            get { return _question.Category; }
            set
            {
                _question.Category = value;
                RaisePropertyChanged(() => Category);
            }
        }

        public virtual List<Answer> Answers
        {
            get { return _question.Answers ?? (_question.Answers = new List<Answer>()); }
            set
            {
                _question.Answers = value;
                RaisePropertyChanged(() => Answers);
            }
        }

        public int AmountOfAnswers
        {
            get { return Answers.Count; }
        }
    }
}
