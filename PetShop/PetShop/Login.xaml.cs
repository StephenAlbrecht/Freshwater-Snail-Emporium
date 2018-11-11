using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace PetShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<User> Users = new List<User>();
        XmlSerializer xmler = new XmlSerializer(typeof(List<User>));

        public MainWindow()
        {
            InitializeComponent();
            ReadInUsers();
        }

        private void ReadInUsers()
        {
            string path = "Users.xml";
            if (File.Exists(path))
            {
                using (FileStream readStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    Users = xmler.Deserialize(readStream) as List<User>;
                }
            }
        }

        private void CreateAccountClicked(object sender, RoutedEventArgs e)
        {
            AddUser addUserWindow = new AddUser(ref Users);
            addUserWindow.ShowDialog();
        }

        private void LogInClicked(object sender, RoutedEventArgs e)
        {
            if (Authenticated())
            {
                User user = Users.FirstOrDefault(x => x.Username == UsernameEntry.Text
                    && x.Password == PasswordEntry.Password);
                if (user.Seller)
                {
                    SellerHome sellerWindow = new SellerHome(ref user);
                    sellerWindow.Show();
                    Close();
                }
                else
                {

                }
            }
        }

        private bool Authenticated()
        {
            bool valid = true;
            if (string.IsNullOrWhiteSpace(UsernameEntry.Text))
            {
                UsernameEntry.BorderBrush = Brushes.Red;
                valid = false;
            }
            if (string.IsNullOrWhiteSpace(PasswordEntry.Password))
            {
                PasswordEntry.BorderBrush = Brushes.Red;
                valid = false;
            }
            if (!valid) return false;
            User tmp = Users.FirstOrDefault(x => x.Username == UsernameEntry.Text 
                    && x.Password == PasswordEntry.Password);
            if (tmp == null)
            {
                valid = false;
                MessageBox.Show("Invalid account credentials. Please try again.", "Login Error");
            }
            return valid;
        }

        private void TextBoxChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.BorderBrush = Brushes.DarkGray;
        }

        private void PasswordBoxChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox pb = (sender as PasswordBox);
            pb.BorderBrush = Brushes.DarkGray;
        }
    }
}
