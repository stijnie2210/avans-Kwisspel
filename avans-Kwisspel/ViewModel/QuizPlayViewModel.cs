using avans_Kwisspel.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace avans_Kwisspel.ViewModel
{
    public class QuizPlayViewModel : ViewModelBase
    {

        private DatabaseContext _databaseContext;

        public ICommand GiveAnswerCommand;

        public QuizPlayViewModel()
        {
            _databaseContext = new DatabaseContext();

            SelectedQuestion = new QuestionVM();
            SelectedQuiz = new QuizVM();

            IEnumerable<QuestionVM> questions = _databaseContext.Questions.ToList().Select(q => new QuestionVM(q));
            IEnumerable<QuizVM> quizzes = _databaseContext.Quizzes.ToList().Select(qz => new QuizVM(qz));
            IEnumerable<AnswerVM> answers = _databaseContext.Answers.ToList().Select(a => new AnswerVM(a));

            Questions = new ObservableCollection<QuestionVM>(questions);
            Quizzes = new ObservableCollection<QuizVM>(quizzes);

            GiveAnswerCommand = new RelayCommand(GiveAnswer);
        }

        private void GiveAnswer()
        {
            // hehe
        }

        private ObservableCollection<QuestionVM> _questions;
        private ObservableCollection<QuizVM> _quizzes;
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

        public ObservableCollection<QuizVM> Quizzes
        {
            get { return _quizzes; }
            set
            {
                _quizzes = value;
                RaisePropertyChanged(() => Quizzes);
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
        private QuizVM _selectedQuiz;
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

        private void LoadQuestions()
        {
            Questions = new ObservableCollection<QuestionVM>(SelectedQuiz.Questions.ToList().Select(question => new QuestionVM(question)));
            SelectedQuestion = Questions.FirstOrDefault();
        }

        public QuizVM SelectedQuiz
        {
            get { return _selectedQuiz; }
            set
            {
                _selectedQuiz = value;
                RaisePropertyChanged(() => SelectedQuiz);

                LoadQuestions();
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
            }
        }
    }
}
