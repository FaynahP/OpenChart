using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenChart.ViewModel;

namespace OpenChart.Model
{
    public class ClientModel : ObservableObject
    {
        private string _clientFN = string.Empty;
        private string _clientLN = string.Empty;
        private string _contactNum = string.Empty;
        private string _emergencyCon = string.Empty;

        public string Client_FName
        {
            get { return _clientFN; }
            set
            {
                if (_clientFN != value)
                {
                    _clientFN = value;
                    OnPropertyChanged(nameof(Client_FName));
                }
            }
        }

        public string Client_LName
        {
            get { return _clientLN; }
            set
            {
                if (_clientLN != value)
                {
                    _clientLN = value;
                    OnPropertyChanged(nameof(Client_LName));
                }
            }
        }

        public string Contact_Number
        {
            get { return _contactNum; }
            set
            {
                if (_contactNum != value)
                {
                    _contactNum = value;
                    OnPropertyChanged(nameof(Contact_Number));
                }
            }
        }

        public string Emergency_Contact
        {
            get { return _emergencyCon; }
            set
            {
                if (_emergencyCon != value)
                {
                    _emergencyCon = value;
                    OnPropertyChanged(nameof(Emergency_Contact));
                }
            }
        }
    }
}
