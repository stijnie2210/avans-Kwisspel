using avans_Kwisspel.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace avans_Kwisspel.ViewModel
{
    public class CategoryVM : ViewModelBase
    {
        public Category _category;

        public CategoryVM()
        {
            _category = new Category();
        }

        public CategoryVM(Category category)
        {
            _category = category;
        }

        public int Id
        {
            get { return _category.Id; }
        }

        public string Text
        {
            get { return _category.Text; }
            set
            {
                _category.Text = value;
                RaisePropertyChanged(() => Text);
            }
        }

        public virtual List<Question> Questions
        {
            get { return _category.Questions; }
            set
            {
                _category.Questions = value;
                RaisePropertyChanged(() => Questions);
            }
        }

        public Category toCategory()
        {
            return _category;
        }
    }
}
