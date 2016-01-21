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

        public MainViewModel()
        {
            _databaseContext = new DatabaseContext();

            OpenQuestionOverview = new RelayCommand(openQuestionOverview);

            _databaseContext.SaveChanges();
        }

        private void openQuestionOverview()
        {
            QuestionOverview qov = new QuestionOverview();
            qov.Show();
        }

    }
}