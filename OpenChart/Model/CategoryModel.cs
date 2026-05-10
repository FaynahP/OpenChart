using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenChart.ViewModel;

namespace OpenChart.Model
{
    internal class CategoryModel : ObservableObject
    {
        private string _categoryID = string.Empty;
        private string _categorydesc = string.Empty;

        public string Category_ID
        {
            get { return _categoryID; }
            set
            {
                if (_categoryID != value)
                {
                    _categoryID = value;
                    OnPropertyChanged(nameof(Category_ID));
                }
            }
        }

        public string Category_Description
        {
            get { return _categorydesc; }
            set
            {
                if (_categorydesc != value)
                {
                    _categorydesc = value;
                    OnPropertyChanged(nameof(Category_Description));
                }
            }
        }
    }
}