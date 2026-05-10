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
    public class SupplierQAViewModel : ObservableObject
    {
        public UserModel CurrentUser { get; set; }
        public SupplierModel addSupplier { get; set; }
        public ICommand AddSupplierCommand { get; set; }
        public ICommand CancelAddSupplier { get; set; }

        public SupplierQAViewModel(UserModel currentuser)
        {
            CurrentUser = currentuser;
            addSupplier = new SupplierModel();
            AddSupplierCommand = new AsyncRelayCommand(ExecuteAddSupplier);
            CancelAddSupplier = new RelayCommand(ExecuteCancelAddSupplier);
        }

        public async Task ExecuteAddSupplier(object parameter)
        {
            string connectionString = @"Data Source=XIAN;Initial Catalog=OpenChartDB;Trusted_Connection=True;TrustServerCertificate=True;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    string lastID = "SP00";
                    string getLastID = "SELECT MAX(Supp_ID) FROM Supplier";
                    using (SqlCommand idCmd = new SqlCommand(getLastID, connection))
                    {
                        var result = await idCmd.ExecuteScalarAsync();
                        if (result != null) lastID = result.ToString();
                    }

                    int number = int.Parse(lastID.Substring(2));
                    string newID = "SP" + (number + 1).ToString("D2");

                    string query = "INSERT INTO Supplier (Supp_ID, Supp_Name, Contact_Number, Email_Address) " +
                                   "VALUES (@suppID, @suppName, @contactNum, @emailAddress)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@suppID", newID);
                        command.Parameters.AddWithValue("@suppName", addSupplier.Supp_Name);
                        command.Parameters.AddWithValue("@contactNum", addSupplier.Contact_Number);
                        command.Parameters.AddWithValue("@emailAddress", addSupplier.Email_Address);

                        int rowsAffected = await command.ExecuteNonQueryAsync();
                        if (rowsAffected != 0)
                            MessageBox.Show("Supplier " + newID + " added!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    addSupplier.Supp_Name = string.Empty;
                    addSupplier.Contact_Number = string.Empty;
                    addSupplier.Email_Address = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        public void ExecuteCancelAddSupplier(object parameter)
        {
            addSupplier.Supp_Name = string.Empty;
            addSupplier.Contact_Number = string.Empty;
            addSupplier.Email_Address = string.Empty;

            var window = parameter as Window;
            var dashboardViewModel = new DashboardViewModel(CurrentUser);
            var dashboardWindow = new View.Dashboard();
            dashboardWindow.DataContext = dashboardViewModel;
            dashboardWindow.Show();
            window?.Close();
        }
    }
}