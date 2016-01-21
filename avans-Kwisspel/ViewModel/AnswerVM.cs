using avans_Kwisspel.Model;
using GalaSoft.MvvmLight;

namespace avans_Kwisspel.ViewModel
{
    public class AnswerVM : ViewModelBase
    {
        public Answer _answer;

        public AnswerVM()
        {
            _answer = new Answer();
        }

        public AnswerVM(Answer answer)
        {
            _answer = answer;
        }

        public int Id
        {
            get { return _answer.Id; }
        }

        public string Text
        {
            get { return _answer.Text; }
            set
            {
                _answer.Text = value;
                RaisePropertyChanged(() => Text);
            }
        }

        public bool isCorrect
        {
            get { return _answer.isCorrect; }
            set
            {
                _answer.isCorrect = value;
                RaisePropertyChanged(() => isCorrect);
            }
        }

        public virtual Question Question
        {
            get { return _answer.Question; }
            set
            {
                _answer.Question = value;
                RaisePropertyChanged(() => Question);
            }
        }

        public Answer toAnswer()
        {
            return _answer;
        }
    }
}
