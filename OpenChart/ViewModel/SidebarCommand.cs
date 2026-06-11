using OpenChart.Model;
using OpenChart.View;
using OpenChart.View.Staff;
using OpenChart.View.InvHandlers;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OpenChart.ViewModel
{
    public class SideBarCommand : INotifyPropertyChanged
    {
        private UserModel _currentUser;
        public event PropertyChangedEventHandler PropertyChanged; // Implement INotifyPropertyChanged for UI updates

        private bool _isMenuOpen = true;
        public bool IsMenuOpen
        {
            get => _isMenuOpen;
            set
            {
                _isMenuOpen = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsMenuOpen))); // Notify UI of change
            }
        }

        // --- STAFF COMMANDS ---
        public ICommand ToggleMenuCommand { get; set; }
        public ICommand DashCommand { get; set; }
        public ICommand MedicineCommand { get; set; }
        public ICommand ClientCommand { get; set; }
        public ICommand TransCommand { get; set; }
        public ICommand SuBCommand { get; set; }
        public ICommand AddTransCommand { get; set; }
        public ICommand ReportCommand { get; set; }

        // --- INV HANDLERS COMMANDS ---
        public ICommand IHDashCommand { get; set; }
        public ICommand IHvMedicineCommand { get; set; }
        public ICommand IHClientCommand { get; set; }
        public ICommand IHTransCommand { get; set; }
        public ICommand IHSuBCommand { get; set; }
        public ICommand SuppCommand { get; set; }
        public ICommand QACommand { get; set; }

        private Grid _mainGrid;
        private Border _mainBorder;

        public SideBarCommand(UserModel currentUser)
        {
            _currentUser = currentUser;
            // STAFF COMMANDS
            ToggleMenuCommand = new RelayCommand(param => ToggleMenu());
            DashCommand = new RelayCommand(param => Navigate(new DashboardUCStaff()));
            ClientCommand = new RelayCommand(param => Navigate(new ClientUCStaff()));
            MedicineCommand = new RelayCommand(param => Navigate(new MedicineUCStaff())); 
            TransCommand = new RelayCommand(param => Navigate(new TransactionsUCStaff()));
            SuBCommand = new RelayCommand(param => Navigate(new SupplyBatchesUCStaff()));
            AddTransCommand = new RelayCommand(param => Navigate(new AddTransactionsUCStaff()));
            ReportCommand = new RelayCommand(param => OpenReportWindow());  // Open Window
            // INV HANDLERS COMMANDS
            IHClientCommand = new RelayCommand(param => Navigate(new ClientUCInv()));
            IHDashCommand = new RelayCommand(param => Navigate(new DashboardUCInv()));
            IHSuBCommand = new RelayCommand(param => Navigate(new SupplyBatchesUCInv()));
            IHTransCommand = new RelayCommand(param => Navigate(new TransactionsUCInv()));
            IHvMedicineCommand = new RelayCommand(param => Navigate(new MedicineUCInv()));
            SuppCommand = new RelayCommand(param => Navigate(new SuppliersUCInv()));
            QACommand = new RelayCommand(param => InvQACommand());


        }
        private void InvQACommand()
        {
            var qaWindow = new Client_QuickAdd();
            qaWindow.Show();
        }
        private void OpenReportWindow()
        {
            var reportWindow = new ReportIssue();
            reportWindow.Show();
        }
        public void SetWindow(Grid mainGrid, Border mainBorder)
        {
            _mainGrid = mainGrid;
            _mainBorder = mainBorder;
        }

        // --- TOGGLE LOGIC ---
        private void ToggleMenu()
        {
            if (_mainGrid == null) return; // Ensure _mainGrid is set before toggling

            IsMenuOpen = !IsMenuOpen; // Toggle the menu state
            var sidebarColumn = _mainGrid.ColumnDefinitions[0]; 
            sidebarColumn.Width = IsMenuOpen ? new GridLength(156) : new GridLength(0); // Adjust the width of the sidebar column based on IsMenuOpen
        }
      

        // --- NAVIGATION LOGIC ---
        private void Navigate(UserControl page)
        {
            if (_mainBorder == null) return;
            _mainBorder.Child = page;
        }
    }
}