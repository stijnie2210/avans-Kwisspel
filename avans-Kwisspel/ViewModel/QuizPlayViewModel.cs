using avans_Kwisspel.Data;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avans_Kwisspel.ViewModel
{
    public class QuizPlayViewModel : ViewModelBase
    {

        private DatabaseContext _databaseContext;

        public QuizPlayViewModel()
        {
            _databaseContext = new DatabaseContext();

            SelectedQuestion = new QuestionVM();

            IEnumerable<QuestionVM> questions = _databaseContext.Questions.ToList().Select(q => new QuestionVM(q));
            IEnumerable<AnswerVM> answers = _databaseContext.Answers.ToList().Select(a => new AnswerVM(a));

            Questions = new ObservableCollection<QuestionVM>(questions);
        }

        private ObservableCollection<QuestionVM> _questions;
        private ObservableCollection<AnswerVM> _answers;

        public ObservableCollection<QuestionVM> Questions
        {
            get { return _questions; }
            set
            {
                _questions = value;
                RaisePropertyChanged(() => Questions);
            }
        }

        public ObservableCollection<AnswerVM> Answers
        {
            get { return _answers; }
            set
            {
                _answers = value;
                RaisePropertyChanged(() => Answers);
            }
        }

        private QuestionVM _selectedQuestion;
        private AnswerVM _selectedAnswer;

        public QuestionVM SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                _selectedQuestion = value;
                RaisePropertyChanged(() => SelectedQuestion);
                loadAnswers();
            }
        }

        public AnswerVM SelectedAnswer
        {
            get { return _selectedAnswer; }
            set
            {
                _selectedAnswer = value;
                RaisePropertyChanged(() => SelectedAnswer);
            }
        }

        private void loadAnswers()
        {
            if (SelectedQuestion != null)
            {
                Answers = new ObservableCollection<AnswerVM>(SelectedQuestion.Answers.ToList().Select(answer => new AnswerVM(answer)));
                SelectedAnswer = Answers.FirstOrDefault();
                SelectedAnswer = new AnswerVM();
            }
        }
    }
}
