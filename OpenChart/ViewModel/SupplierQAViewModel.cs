using OpenChart.ViewModel;
using Microsoft.Data.SqlClient;
using OpenChart.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OpenChart.ViewModel
{
    internal class SupplierQAViewModel : ObservableObject
    {
        public UserModel CurrentUser { get; set; }

        public SupplierQAViewModel(UserModel currentuser)
        {
            //addClient = new ClientModel();

            CurrentUser = currentuser;
            //AddClientCommand = new AsyncRelayCommand(ExecuteAddClient);
            //CancelAddClient = new RelayCommand(ExecuteCancelAddClient);

        }
    }
}
