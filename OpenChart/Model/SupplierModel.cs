using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenChart.ViewModel;

namespace OpenChart.Model
{
    public class SupplierModel : ObservableObject
    {
        private string _supp_ID = string.Empty;
        private string _supp_Name = string.Empty;
        private string _contact_number = string.Empty;
        private string _email = string.Empty;

        public string Supp_ID
        {
            get { return _supp_ID; }
            set
            {
                if (_supp_ID != value)
                {
                    _supp_ID = value;
                    OnPropertyChanged(nameof(Supp_ID));
                }
            }
        }

        public string Supp_Name
        {
            get { return _supp_Name; }
            set
            {
                if (_supp_Name != value)
                {
                    _supp_Name = value;
                    OnPropertyChanged(nameof(Supp_Name));
                }
            }
        }

        public string Contact_Number
        {
            get { return _contact_number; }
            set
            {
                if (_contact_number != value)
                {
                    _contact_number = value;
                    OnPropertyChanged(nameof(Contact_Number));
                }
            }
        }

        public string Email_Address
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email_Address));
                }
            }
        }
    }
}