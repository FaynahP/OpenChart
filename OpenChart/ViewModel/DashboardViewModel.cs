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

        public DashboardViewModel(UserModel currentuser)
        {
            CurrentUser = currentuser;
        }
    }
}
