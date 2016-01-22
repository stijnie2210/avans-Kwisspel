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
    public class QuizOverviewViewModel : ViewModelBase
    {
        private DatabaseContext _dataContext;

        public ICommand AddQuiz { get; set; }
        public ICommand DeleteQuiz { get; set; }
        public ICommand Clear { get; set; }

        public QuizOverviewViewModel()
        {
            _dataContext = new DatabaseContext();

            AddQuiz = new RelayCommand(doAddQuiz);
            DeleteQuiz = new RelayCommand(doDeleteQuiz);
            Clear = new RelayCommand(doClear);

            SelectedQuiz = new QuizVM();
            SelectedQuestion = new QuestionVM();

            IEnumerable<QuizVM> quizzes = _dataContext.Quizzes.ToList().Select(q => new QuizVM(q));
            IEnumerable<QuestionVM> questions = _dataContext.Questions.ToList().Select(q => new QuestionVM(q));

            Quizzes = new ObservableCollection<QuizVM>(quizzes);
            Questions = new ObservableCollection<QuestionVM>(questions);

        }

        private ObservableCollection<QuizVM> _quizzes;
        private ObservableCollection<QuestionVM> _questions;

        public ObservableCollection<QuizVM> Quizzes
        {
            get { return _quizzes; }
            set
            {
                _quizzes = value;
                RaisePropertyChanged(() => Quizzes);
            }
        }

        public ObservableCollection<QuestionVM> Questions
        {
            get { return _questions; }
            set
            {
                _questions = value;
                RaisePropertyChanged(() => Questions);
            }
        }

        private QuizVM _selectedQuiz;
        private QuestionVM _selectedQuestion;

        public QuizVM SelectedQuiz
        {
            get { return _selectedQuiz; }
            set
            {
                _selectedQuiz = value;
                RaisePropertyChanged(() => SelectedQuiz);
            }
        }

        public QuestionVM SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                _selectedQuestion = value;
                RaisePropertyChanged(() => SelectedQuestion);
            }
        }

        private void doAddQuiz()
        {

        }

        private void doDeleteQuiz()
        {

        }

        private void doClear()
        {
            SelectedQuiz = new QuizVM();
            SelectedQuestion = new QuestionVM();
        }
    }
}
