using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace avans_Kwisspel.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<QuestionViewModel>();
            SimpleIoc.Default.Register<QuizOverviewViewModel>();
            SimpleIoc.Default.Register<QuizPlayViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public QuestionViewModel Question
        {
            get
            {
                return ServiceLocator.Current.GetInstance<QuestionViewModel>();
            }
        }

        public QuizOverviewViewModel Quiz
        {
            get
            {
                return ServiceLocator.Current.GetInstance<QuizOverviewViewModel>();
            }
        }

        public QuizPlayViewModel Quizplay
        {
            get
            {
                return ServiceLocator.Current.GetInstance<QuizPlayViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}