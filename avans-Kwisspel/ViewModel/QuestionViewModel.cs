﻿using avans_Kwisspel.Data;
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
        public ICommand SaveCommand { get; set; }

        public QuestionViewModel()
        {
            _databaseContext = new DatabaseContext();

            AddAnswer = new RelayCommand(doAddAnswer);
            AddQuestion = new RelayCommand(doAddQuestion);
            DeleteQuestion = new RelayCommand(doDeleteQuestion);
            DeleteAnswer = new RelayCommand(doDeleteAnswer);
            Clear = new RelayCommand(doClear);
            SaveCommand = new RelayCommand(doSave);

            SelectedCategory = new CategoryVM();
            SelectedQuestion = new QuestionVM();
            SelectedAnswer = new AnswerVM();
            

            IEnumerable<QuestionVM> questions = _databaseContext.Questions.ToList().Select(question => new QuestionVM(question));
            IEnumerable<CategoryVM> categories = _databaseContext.Categories.ToList().Select(category => new CategoryVM(category));
            IEnumerable<AnswerVM> answers = _databaseContext.Answers.ToList().Select(answer => new AnswerVM(answer));
            IEnumerable<QuizVM> quizzes = _databaseContext.Quizzes.ToList().Select(quiz => new QuizVM(quiz));

            Questions = new ObservableCollection<QuestionVM>(questions);
            Categories = new ObservableCollection<CategoryVM>(categories);
            Quizzes = new ObservableCollection<QuizVM>(quizzes);

            
        }

        #region Observable Collections
        private ObservableCollection<QuestionVM> _questions;
        private ObservableCollection<CategoryVM> _categories;
        private ObservableCollection<AnswerVM> _answers;
        private ObservableCollection<QuizVM> _quizzes;

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

        public ObservableCollection<QuizVM> Quizzes
        {
            get { return _quizzes; }
            set
            {
                _quizzes = value;
                RaisePropertyChanged(() => Quizzes);
            }
        }
        #endregion

        #region Selected VMs

        private QuestionVM _selectedQuestion;
        private CategoryVM _selectedCategory;
        private AnswerVM _selectedAnswer;
        private QuizVM _selectedQuiz;

        public QuestionVM SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                _selectedQuestion = value;
                RaisePropertyChanged(() => SelectedQuestion);
                SelectedCategory = null;
                SelectedQuiz = null;
                LoadAnswers();
                
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

        public QuizVM SelectedQuiz
        {
            get { return _selectedQuiz; }
            set
            {
                _selectedQuiz = value;
                RaisePropertyChanged(() => SelectedQuiz);
            }
        }
        #endregion

        #region private voids for ICommands
        private void doAddQuestion()
        {
            if (SelectedQuestion == null || SelectedCategory == null || SelectedQuiz == null)
            {
                SelectedQuestion = new QuestionVM();
                SelectedCategory = new CategoryVM();
                SelectedQuiz = new QuizVM();
            }

            if (SelectedCategory.Text != null && SelectedQuiz.Text != null)
            {
                SelectedQuestion.Category = SelectedCategory.toCategory();
                SelectedQuestion.Quiz = SelectedQuiz.toQuiz();
            }
            else
            {
                MessageBox.Show("Selecteer voor de vraag zowel een categorie als een kwis", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SelectedQuestion.Text == null)
            {
                MessageBox.Show("Er is geen naam gegeven aan de vraag, vul de naam voor een nieuwe vraag in.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var question = _databaseContext.Questions.Find(SelectedQuestion.Id);

            if (question == null)
            {
                Questions.Add(SelectedQuestion);
                _databaseContext.Questions.Add(SelectedQuestion.toQuestion());
                _databaseContext.SaveChanges();
            }
            else
            {
                MessageBox.Show("Vraag bestaat al, druk op clear en vul dan de naam voor een nieuwe vraag in, of druk op opslaan als u de gegevens wilt opslaan.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            SelectedCategory = null;
            SelectedQuiz = null;
        }

        private void answerClear()
        {
            SelectedAnswer = new AnswerVM();
            SelectedCategory = null;
            SelectedQuiz = null;
        }
        private void doAddAnswer()
        {
            var question = _databaseContext.Questions.Find(SelectedQuestion.Id);

            if (question.Answers.Count == 4)
            {
                MessageBox.Show("Aan een vraag kunnen niet meer dan vier antwoorden worden gegeven.", "Waarschuwing", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                answerClear();
                return;
            }

            if (question == null)
            {
                return;
            }
            if (SelectedAnswer == null)
            {
                answerClear();
            }
            SelectedAnswer.Question = SelectedQuestion.toQuestion();
            SelectedQuestion.Answers.Add(SelectedAnswer.toAnswer());
            Answers.Add(SelectedAnswer);
            _databaseContext.Answers.Add(SelectedAnswer.toAnswer());
            _databaseContext.SaveChanges();
            SelectedAnswer = null;
            doClear();
        }

        private void Cancel(Window window)
        {
            if (window != null)
                window.Close();
        }

        private void LoadAnswers()
        {
            if (SelectedQuestion != null)
            {
                Answers = new ObservableCollection<AnswerVM>(SelectedQuestion.Answers.ToList().Select(answer => new AnswerVM(answer)));
                SelectedAnswer = Answers.FirstOrDefault();
                SelectedAnswer = new AnswerVM();
            }
        }

        private void doSave()
        {
            foreach (QuestionVM selectedItem in Questions)
            {
                var item = _databaseContext.Questions.Find(selectedItem.Id);
                if (item == null)
                {
                    _databaseContext.Questions.Add(selectedItem.toQuestion());
                    _databaseContext.SaveChanges();
                }
                else
                {
                    if (SelectedCategory != null)
                    {
                        SelectedQuestion.Category = SelectedCategory.toCategory();
                    }

                    if (SelectedQuiz != null)
                    {
                        SelectedQuestion.Quiz = SelectedQuiz.toQuiz();
                    }
                    _databaseContext.Entry(item).CurrentValues.SetValues(selectedItem);
                    _databaseContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    _databaseContext.SaveChanges();
                }
            }
            MessageBox.Show("De gegevens zijn opgeslagen!", "Succes!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion
    }
}
