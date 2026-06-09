using OpenChart.Model;
using OpenChart.View;
using OpenChart.View.Staff;
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

        // --- COMMANDS ---
        public ICommand ToggleMenuCommand { get; }
        public ICommand QA_ClientCommand { get; }
        public ICommand DashCommand { get; }
        public ICommand MedicineCommand { get; }
        public ICommand ClientCommand { get; }
        public ICommand TransCommand { get; }
        public ICommand SuBCommand { get; }
        public ICommand SuppCommand { get; }

        private Grid _mainGrid;
        private Border _mainBorder;

        public SideBarCommand(UserModel currentUser)
        {
            _currentUser = currentUser;
            ToggleMenuCommand = new RelayCommand(param => ToggleMenu());
            DashCommand = new RelayCommand(param => Navigate(new DashboardUCStaff()));
            ClientCommand = new RelayCommand(param => Navigate(new ClientUCStaff()));
            MedicineCommand = new RelayCommand(param => Navigate(new MedicineUCStaff())); 
            TransCommand = new RelayCommand(param => Navigate(new TransactionsUCStaff()));
            SuBCommand = new RelayCommand(param => Navigate(new SupplyBatchesUCStaff()));
           
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