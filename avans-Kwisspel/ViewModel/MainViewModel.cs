using avans_Kwisspel.Data;
using avans_Kwisspel.Model;
using avans_Kwisspel.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;

namespace avans_Kwisspel.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private DatabaseContext _databaseContext;

        public ICommand OpenQuestionOverview { get; set; }
        public ICommand OpenQuizOverview { get; set; }

        public MainViewModel()
        {
            _databaseContext = new DatabaseContext();

            OpenQuestionOverview = new RelayCommand(openQuestionOverview);
            OpenQuizOverview = new RelayCommand(openQuizOverview);

            _databaseContext.SaveChanges();
        }

        private void openQuestionOverview()
        {
            QuestionOverview qov = new QuestionOverview();
            qov.Show();
        }

        private void openQuizOverview()
        {
            QuizOverview qv = new QuizOverview();
            qv.Show();
        }

    }
}