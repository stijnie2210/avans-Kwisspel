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
    public class QuestionViewModel : ViewModelBase
    {
        private DatabaseContext _databaseContext;

        public QuestionViewModel()
        {
            _databaseContext = new DatabaseContext();

            IEnumerable<QuestionVM> questions = _databaseContext.Questions.ToList().Select(question => new QuestionVM(question));
            Questions = new ObservableCollection<QuestionVM>(questions);
        }

        private ObservableCollection<QuestionVM> _questions;

        public ObservableCollection<QuestionVM> Questions
        {
            get { return _questions; }
            set
            {
                _questions = value;
                RaisePropertyChanged(() => Questions);
            }
        }

        private QuestionVM _selectedQuestion;

        public QuestionVM SelectedQuestion
        {
            get { return _selectedQuestion; }
            set
            {
                _selectedQuestion = value;
                RaisePropertyChanged(() => SelectedQuestion);
            }
        }
    }
}
