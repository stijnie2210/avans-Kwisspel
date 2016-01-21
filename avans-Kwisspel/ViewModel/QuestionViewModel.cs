using avans_Kwisspel.Data;
using avans_Kwisspel.Model;
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
    public class QuestionViewModel : ViewModelBase
    {
        private DatabaseContext _databaseContext;
        public ICommand AddAnswer { get; set; }
        public ICommand AddQuestion { get; set; }
        public ICommand DeleteQuestion { get; set; }
        public ICommand DeleteAnswer { get; set; }
        public ICommand Clear { get; set; }

        public QuestionViewModel()
        {
            _databaseContext = new DatabaseContext();

            AddAnswer = new RelayCommand(doAddAnswer);
            AddQuestion = new RelayCommand(doAddQuestion);
            DeleteQuestion = new RelayCommand(doDeleteQuestion);
            DeleteAnswer = new RelayCommand(doDeleteAnswer);
            Clear = new RelayCommand(doClear);

            SelectedCategory = new CategoryVM();
            SelectedQuestion = new QuestionVM();
            SelectedAnswer = new AnswerVM();
            

            IEnumerable<QuestionVM> questions = _databaseContext.Questions.ToList().Select(question => new QuestionVM(question));
            IEnumerable<CategoryVM> categories = _databaseContext.Categories.ToList().Select(category => new CategoryVM(category));
            IEnumerable<AnswerVM> answers = _databaseContext.Answers.ToList().Select(answer => new AnswerVM(answer));

            Questions = new ObservableCollection<QuestionVM>(questions);
            Categories = new ObservableCollection<CategoryVM>(categories);

            Answers = new ObservableCollection<AnswerVM>(answers);

            
        }

        private ObservableCollection<QuestionVM> _questions;
        private ObservableCollection<CategoryVM> _categories;
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

        public ObservableCollection<CategoryVM> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                RaisePropertyChanged(() => Categories);
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
        private CategoryVM _selectedCategory;
        private AnswerVM _selectedAnswer;

        public QuestionVM SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                _selectedQuestion = value;
                RaisePropertyChanged(() => SelectedQuestion);
            }
        }

        public CategoryVM SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                RaisePropertyChanged(() => SelectedCategory);
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

        private void doAddQuestion()
        {
            if (SelectedQuestion == null)
            {
                SelectedQuestion = new QuestionVM();
            }
            SelectedQuestion.Quiz = _databaseContext.Quizzes.FirstOrDefault();
            SelectedQuestion.Category = SelectedCategory.toCategory();

            var question = _databaseContext.Questions.Find(SelectedQuestion.Id);
            if (question == null)
            {
                Questions.Add(SelectedQuestion);
                _databaseContext.Questions.Add(SelectedQuestion.toQuestion());
                _databaseContext.SaveChanges();
            }
        }

        private void doDeleteQuestion()
        {
            if (SelectedQuestion != null)
            {
                var question = _databaseContext.Questions.Find(SelectedQuestion.Id);
                if (question != null)
                {
                    _databaseContext.Questions.Attach(question);
                    _databaseContext.Questions.Remove(question);
                    _databaseContext.SaveChanges();
                    Questions.Remove(SelectedQuestion);
                    SelectedQuestion = new QuestionVM();
                }
            }
        }

        private void doDeleteAnswer()
        {
            if (SelectedAnswer != null)
            {
                var answer = _databaseContext.Answers.Find(SelectedAnswer.Id);
                if (answer != null)
                {
                    _databaseContext.Answers.Attach(answer);
                    _databaseContext.Answers.Remove(answer);
                    _databaseContext.SaveChanges();
                    Answers.Remove(SelectedAnswer);
                    SelectedAnswer = new AnswerVM();
                }
            }
        }

        private void doClear()
        {
            SelectedQuestion = new QuestionVM();
            SelectedAnswer = new AnswerVM();
        }
        private void doAddAnswer()
        {
            var question = _databaseContext.Questions.Find(SelectedQuestion.Id);

            if (question == null)
            {
                return;
            }
            if (SelectedAnswer == null)
            {
                SelectedAnswer = new AnswerVM();
            }
            SelectedAnswer.Question = SelectedQuestion.toQuestion();
            SelectedQuestion.Answers.Add(SelectedAnswer.toAnswer());
            Answers.Add(SelectedAnswer);
            _databaseContext.Answers.Add(SelectedAnswer.toAnswer());
            _databaseContext.SaveChanges();
        }

        private void Cancel(Window window)
        {
            if (window != null)
                window.Close();
        }
    }
}
