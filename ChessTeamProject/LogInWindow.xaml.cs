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
using System.Security.Cryptography;
using System.Data.SQLite;
using System.Data.SqlClient;

namespace ChessTeamProject
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        static string userName; static string password;
        static string currentWindow;
        private static string connectionString = "Data Source=UserDB.db";

        public LogInWindow()
        {
            InitializeComponent();

            StartBorder.Background = (Brush)new BrushConverter().ConvertFromString("White");
            RegistrationBorder.Background = (Brush)new BrushConverter().ConvertFromString("White");
            LogInBorder.Background = (Brush)new BrushConverter().ConvertFromString("White");
        }

        private void SetVisibility(int num)
        {
            switch (num)
            {
                case 1:
                    StartBorder.Visibility = Visibility.Visible;
                    StartBorder.IsEnabled = true;
                    BackButton.Visibility = Visibility.Hidden;
                    BackButton.IsEnabled = false;
                    break;

                case 2:
                    StartBorder.Visibility = Visibility.Hidden;
                    StartBorder.IsEnabled = false;
                    BackButton.Visibility = Visibility.Visible;
                    BackButton.IsEnabled = true;
                    break;

                case 3:
                    LogInBorder.Visibility = Visibility.Visible;
                    LogInBorder.IsEnabled = true;
                    currentWindow = "Log In";
                    break;

                case 4:
                    LogInBorder.Visibility = Visibility.Hidden;
                    LogInBorder.IsEnabled = false;
                    break;

                case 5:
                    RegistrationBorder.Visibility = Visibility.Visible;
                    RegistrationBorder.IsEnabled = true;
                    currentWindow = "Sign In";
                    break;

                case 6:
                    RegistrationBorder.Visibility = Visibility.Hidden;
                    RegistrationBorder.IsEnabled = false;
                    break;

                default:
                    break;
            }
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            userName = txtUserName.Text;
            password = pwdPassword.Password;
            string confirmPassword = pwdConfirmPassword.Password;

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("All fields must be filled", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Password mismatch", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]); 

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);


            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            string passwordHash = Convert.ToBase64String(hashBytes);

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string countQuery = $"SELECT COUNT(*) FROM Users WHERE Username = '{userName}'";

                using (SQLiteCommand countCommand = new SQLiteCommand(countQuery, connection))
                {
                    int userNameCount = Convert.ToInt32(countCommand.ExecuteScalar());

                    if (userNameCount > 0)
                    {
                        MessageBox.Show("This user already exists in the database");
                        return;
                    }
                }

                connection.Close();
            }

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string sql = "INSERT INTO Users (Username, Password, Salt, TeamName, TotalWin) VALUES (@Username, @Password, @Salt, @TeamName, @TotalWin)";
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Username", userName);
                    command.Parameters.AddWithValue("@Password", passwordHash);
                    command.Parameters.AddWithValue("@Salt", Convert.ToBase64String(salt));
                    command.Parameters.AddWithValue("@TeamName", null);
                    command.Parameters.AddWithValue("@TotalWin", 0);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }


            // SetVisibility(6); SetVisibility(7);       
            Close();
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameL.Text;


            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Users WHERE Username = @Username";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHashedPassword = reader.GetString(1);

                            byte[] storedHashBytes = Convert.FromBase64String(storedHashedPassword);
                            byte[] salt = new byte[16];
                            Array.Copy(storedHashBytes, 0, salt, 0, 16);

                            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(pwdPasswordL.Password, salt, 10000))
                            {
                                byte[] hash = pbkdf2.GetBytes(20);

                                byte[] hashBytes = new byte[36];
                                Array.Copy(salt, 0, hashBytes, 0, 16);
                                Array.Copy(hash, 0, hashBytes, 16, 20);
                                string enteredPasswordHash = Convert.ToBase64String(hashBytes);

                                if (enteredPasswordHash == storedHashedPassword)
                                {
                                    Close();
                                }
                                else
                                {
                                    MessageBox.Show("Incorrect Password");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Incorrect username");
                        }
                    } 
                } 

                connection.Close();
            }
            
        }

        private void ButtonS_Click(object sender, RoutedEventArgs e)
        {

            SetVisibility(2); SetVisibility(5);
        }

        private void ButtonL_Click(object sender, RoutedEventArgs e)
        {
            SetVisibility(2); SetVisibility(3);
        }

        private void BackButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            switch (currentWindow)
            {
                case "Log In":
                    SetVisibility(1);
                    SetVisibility(4);
                    break;

                case "Sign In":
                    SetVisibility(1);
                    SetVisibility(6);
                    break;

                default:
                    break;
            }
        }
    }
}
