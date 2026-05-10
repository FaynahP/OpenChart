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
    public class ClientQAViewModel : ObservableObject
    {
        public static ObservableCollection<ClientModel> Clients { get; set; }
        public ClientModel addClient { get; set; }
        public UserModel CurrentUser { get; set; }

        public ICommand QA_SupplyCommand { get; set; }
        public ICommand QA_SupplierCommand { get; set; }


        public ICommand AddClientCommand { get; set; }
        public ICommand CancelAddClient { get; set; }

        public ClientQAViewModel(UserModel currentuser)
        {
            addClient = new ClientModel();

            CurrentUser = currentuser;
            AddClientCommand = new AsyncRelayCommand(ExecuteAddClient);
            CancelAddClient = new RelayCommand(ExecuteCancelAddClient);
            QA_SupplyCommand = new RelayCommand(MoveToQA_Supply);
            QA_SupplierCommand = new RelayCommand(MoveToQA_Supplier);
        }

        public async Task ExecuteAddClient(object parameter)
        {
            string connectionString = @"Data Source=XIAN;Initial Catalog=OpenChartDB;Trusted_Connection=True;TrustServerCertificate=True;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();


                    string lastID = "CL00";
                    string getLastID = "SELECT MAX(Client_ID) FROM Client";
                    using (SqlCommand idCmd = new SqlCommand(getLastID, connection))
                    {
                        var result = await idCmd.ExecuteScalarAsync();
                        if (result != null) lastID = result.ToString();


                        int number = int.Parse(lastID.Substring(2));
                        string newID = "CL" + (number + 1).ToString("D2");

                        string query = "INSERT INTO Client (Client_ID, Client_FName, Client_LName, Contact_Number, Emergency_Contact, AStatus_ID) " +
                                       "VALUES (@clientID, @clientFN, @clientLN, @contactNum, @emergencyCon, @astatusID)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@clientID", newID);
                            command.Parameters.AddWithValue("@clientFN", addClient.Client_FName);
                            command.Parameters.AddWithValue("@clientLN", addClient.Client_LName);
                            command.Parameters.AddWithValue("@contactNum", addClient.Contact_Number);
                            command.Parameters.AddWithValue("@emergencyCon", addClient.Emergency_Contact);
                            command.Parameters.AddWithValue("@astatusID", "AS1");

                            int rowsAffected = await command.ExecuteNonQueryAsync();
                            if (rowsAffected != 0)
                                MessageBox.Show("Client added!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        }

                        addClient.Client_FName = string.Empty;
                        addClient.Client_LName = string.Empty;
                        addClient.Contact_Number = string.Empty;
                        addClient.Emergency_Contact = string.Empty;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void ExecuteCancelAddClient(object parameter)
        {
            addClient.Client_FName = string.Empty;
            addClient.Client_LName = string.Empty;
            addClient.Contact_Number = string.Empty;
            addClient.Emergency_Contact = string.Empty;

            var window = parameter as Window;
            var dashboardViewModel = new DashboardViewModel(CurrentUser);
            var dashboardWindow = new View.Dashboard();
            dashboardWindow.DataContext = dashboardViewModel;
            dashboardWindow.Show();
            window?.Close();
        }

        private void MoveToQA_Supply(object parameter)
        {
            var window = parameter as Window;
            var supplyQAViewModel = new SupplyQAViewModel(CurrentUser);
            var supplyQAWindow = new View.Supply_QuickAdd();
            supplyQAWindow.DataContext = supplyQAViewModel;
            supplyQAWindow.Show();
            window?.Close();
        }


        private void MoveToQA_Supplier(object parameter)
        {
            var window = parameter as Window;
            var supplierQAViewModel = new SupplierQAViewModel(CurrentUser);
            var supplierQAWindow = new View.Supplier_QuickAdd();
            supplierQAWindow.DataContext = supplierQAViewModel;
            supplierQAWindow.Show();
            window?.Close();
        }
    }
}
