using avans_Kwisspel.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace avans_Kwisspel.ViewModel
{
    public class QuizOverviewViewModel : ViewModelBase
    {
        private DatabaseContext _dataContext;

        public ICommand AddQuiz { get; set; }
        public ICommand DeleteQuiz { get; set; }
        public ICommand DeleteQuestion { get; set; }
        public ICommand Clear { get; set; }
        public ICommand SaveCommand { get; set; }

        public QuizOverviewViewModel()
        {
            _dataContext = new DatabaseContext();

            AddQuiz = new RelayCommand(doAddQuiz);
            DeleteQuiz = new RelayCommand(doDeleteQuiz);
            DeleteQuestion = new RelayCommand(doDeleteQuestion);
            Clear = new RelayCommand(doClear);
            SaveCommand = new RelayCommand(doSave);

            SelectedQuiz = new QuizVM();
            SelectedQuestion = new QuestionVM();

            IEnumerable<QuizVM> quizzes = _dataContext.Quizzes.ToList().Select(q => new QuizVM(q));
            IEnumerable<QuestionVM> questions = _dataContext.Questions.ToList().Select(q => new QuestionVM(q));

            Quizzes = new ObservableCollection<QuizVM>(quizzes);

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
                LoadQuestions();
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
            if (SelectedQuiz == null)
            {
                SelectedQuiz = new QuizVM();
            }

            if (SelectedQuiz.Text == null)
            {
                MessageBox.Show("Er is geen naam gegeven aan de kwis, vul de naam voor een nieuwe kwis in.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var quiz = _dataContext.Quizzes.Find(SelectedQuiz.Id);

            if (quiz == null)
            {
                Quizzes.Add(SelectedQuiz);
                _dataContext.Quizzes.Add(SelectedQuiz.toQuiz());
                _dataContext.SaveChanges();
            }
            else
            {
                MessageBox.Show("Kwis bestaat al, druk op clear en vul dan de naam voor een nieuwe kwis in, of druk op opslaan als u de gegevens wilt opslaan.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void doDeleteQuiz()
        {
            if (SelectedQuiz != null)
            {
                var quiz = _dataContext.Quizzes.Find(SelectedQuiz.Id);

                if (quiz != null)
                {
                    _dataContext.Quizzes.Attach(quiz);
                    _dataContext.Quizzes.Remove(quiz);
                    _dataContext.SaveChanges();
                    Quizzes.Remove(SelectedQuiz);
                    SelectedQuiz = new QuizVM();
                }
            }
        }

        private void doDeleteQuestion()
        {
            if (SelectedQuestion != null)
            {
                var question = _dataContext.Questions.Find(SelectedQuestion.Id);
                if (question != null)
                {
                    _dataContext.Questions.Remove(question);
                    _dataContext.SaveChanges();

                    Questions.Remove(SelectedQuestion);
                    SelectedQuestion = new QuestionVM();
                }
            }
        }

        private void doClear()
        {
            SelectedQuiz = new QuizVM();
            SelectedQuestion = new QuestionVM();
        }

        private void LoadQuestions()
        {
            if (SelectedQuiz != null)
            {
                Questions = new ObservableCollection<QuestionVM>(SelectedQuiz.Questions.ToList().Select(questions => new QuestionVM(questions)));
                SelectedQuestion = Questions.FirstOrDefault();
            }
        }

        private void LoadQuizzes()
        {
            IEnumerable<QuizVM> quizzes = _dataContext.Quizzes.ToList().Select(q => new QuizVM(q));

            Quizzes = new ObservableCollection<QuizVM>(quizzes);

        }

        private void doSave()
        {
            foreach (QuizVM selectedItem in Quizzes)
            {
                var item = _dataContext.Quizzes.Find(selectedItem.Id);
                if (item == null)
                {
                    _dataContext.Quizzes.Add(selectedItem.toQuiz());
                    _dataContext.SaveChanges();
                }
                else
                {
                    _dataContext.Entry(item).CurrentValues.SetValues(selectedItem);
                    _dataContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    _dataContext.SaveChanges();
                }
            }
            MessageBox.Show("De gegevens zijn opgeslagen!", "Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
