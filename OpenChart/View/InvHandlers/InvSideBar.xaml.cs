using OpenChart.Model;
using OpenChart.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OpenChart.View.InvHandlers
{
    /// <summary>
    /// Interaction logic for SideBar.xaml
    /// </summary>
    public partial class InvSideBar : Window
    {
        public UserModel CurrentUser { get; set; }
        public InvSideBar()
        {
            InitializeComponent();
            var viewModel = new SideBarCommand(CurrentUser);
            viewModel.SetWindow(MainGrid, MainContent); 
            DataContext = viewModel;
        }

       
    }
}
