using OpenChart.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OpenChart.ViewModel
{
    public class LoginViewModel : ObservableObject
    {
        public UserModel CurrentUser { get; set; }

        public ICommand LoginCommand { get; set; }

        public ICommand GoToLogin { get; set; }

        public LoginViewModel()
        {
            CurrentUser = new UserModel();
            LoginCommand = new RelayCommand(ExecuteLogin);
            GoToLogin = new RelayCommand(ExecuteGoToLogin);
        }

        private void ExecuteGoToLogin(object parameter)
        {
            var login = new View.Login();
            login.Show();
            Application.Current.MainWindow.Close();
        }

        private async void ExecuteLogin(object parameter)
        {
            var password = parameter as PasswordBox;
            if (password != null)
            {
                CurrentUser.Password = password.Password;   
            }

            string connectionString = @"Data Source=XIAN;Initial Catalog=OpenChartDB;Trusted_Connection=True;TrustServerCertificate=True;";

            //seania's: @"Data Source=XIAN;Initial Catalog=OpenChart;Trusted_Connection=True;TrustServerCertificate=True;"
            //hannah's: @"Data Source=MWEZ\MSSQLSERVER2022;Initial Catalog=OpenChartDatabase;Trusted_Connection=True;TrustServerCertificate=True;"

            bool isLoginValid = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Users WHERE User_ID = @userID AND Password = @password";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@userID", CurrentUser.UserName);
                        command.Parameters.AddWithValue("@password", CurrentUser.Password);
                        await connection.OpenAsync();

                        

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            
                            if (reader.HasRows)
                            { 
                                isLoginValid = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection failed: " + ex.Message);
                return;
            }

            if (isLoginValid)
            {
               var DashboardViewModel = new DashboardViewModel(CurrentUser);
               var login = new View.Dashboard();
                
                
                login.DataContext = DashboardViewModel;
                login.Show();
                Application.Current.MainWindow.Close();
                //MessageBox.Show("Login successful!", "Success",
                //MessageBoxButton.OK, MessageBoxImage.Information);


            }
            else
            {
                MessageBox.Show("Invalid Username or Password.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }
    }
}
