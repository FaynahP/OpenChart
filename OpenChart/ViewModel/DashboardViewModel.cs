using OpenChart.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace OpenChart.ViewModel
{
    class DashboardViewModel : ObservableObject
    {
        public UserModel CurrentUser { get; set; }
        public ICommand QA_ClientCommand { get; set; }
        public ICommand QA_SupplyCommand { get; set; }
        public ICommand QA_SupplierCommand { get; set; }

        public DashboardViewModel(UserModel currentuser)
        {
            CurrentUser = currentuser;
            //QA_ClientCommand = new RelayCommand(MoveToQA_Client);
            
        }

        //private void MoveToQA_Client(object parameter)
        //{
        //    var window = parameter as Window;
        //    var clientQAViewModel = new ClientQAViewModel(CurrentUser);
        //    var clientQAWindow = new View.Client_QuickAdd();
        //    clientQAWindow.DataContext = clientQAViewModel;
        //    clientQAWindow.Show();
        //    window?.Close();
        //}

    }
}
