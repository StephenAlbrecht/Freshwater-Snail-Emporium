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
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace PetShop
{
    /// <summary>
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        List<User> Users = new List<User>();
        XmlSerializer xmler = new XmlSerializer(typeof(List<User>));
        List<string> States = new List<string>
        {
            "AK", "AL", "AR", "AZ", "CA", "CO", "CT", "DC", "DE", "FL",
            "GA", "HI", "IA", "ID", "IL", "IN", "KS", "KY", "LA", "MA",
            "MD", "ME", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE",
            "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "RI",
            "SC", "SD", "TN", "TX", "UT", "VA", "VT", "WA", "WI", "WV", "WY"
        };
        List<string> PaymentTypes = new List<string>
        {
            "Cash", "Check", "Credit Card"
        };

        public AddUser(ref List<User> users)
        {
            InitializeComponent();
            Users = users;
            StateSelector.ItemsSource = States;
            PaymentSelector.ItemsSource = PaymentTypes;
        }

        private void CreateAccountClicked(object sender, RoutedEventArgs e)
        {
            if (Authenticated())
            {
                bool accountType = ((bool)ShopRB.IsChecked) ? false : true;
                User newUser = new User(FirstNameEntry.Text, LastNameEntry.Text, UsernameEntry.Text,
                    PasswordEntry.Password, EmailEntry.Text, AddressEntry.Text, CityEntry.Text, 
                    StateSelector.SelectedValue.ToString(), int.Parse(ZipEntry.Text), 
                    PaymentSelector.SelectedValue.ToString(), accountType);
                Users.Add(newUser);
                WriteUsersToFile();
                MessageBox.Show($"{newUser.ToString()} written to database.", "Account Created");
                Close();
            }
        }

        private void CancelClicked(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WriteUsersToFile()
        {
            string path = "Users.xml";

            if (Users.Count == 0 && File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                using (FileStream filestream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                {
                    xmler.Serialize(filestream, Users);
                }
            }
        }

        private bool Authenticated()
        {
            bool valid = true;
            List<TextBox> tbs = ParentGrid.Children.OfType<TextBox>().ToList();
            foreach (TextBox tb in tbs)
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    tb.BorderBrush = Brushes.Red;
                    valid = false;
                }
            }
            List<PasswordBox> pbs = ParentGrid.Children.OfType<PasswordBox>().ToList();
            foreach (PasswordBox pb in pbs)
            {
                if (string.IsNullOrWhiteSpace(pb.Password))
                {
                    pb.BorderBrush = Brushes.Red;
                    valid = false;
                }
            }
            if (StateSelector.SelectedIndex == -1)
            {
                StateSelectorError.Visibility = Visibility.Visible;
                valid = false;
            }
            if (PaymentSelector.SelectedIndex == -1)
            {
                PaymentError.Visibility = Visibility.Visible;
                valid = false;
            }
            if (!ValidateAlphabetical(FirstNameEntry.Text))
            {
                FirstNameEntry.BorderBrush = Brushes.Red;
                valid = false;
            }
            if (!ValidateAlphabetical(LastNameEntry.Text))
            {
                LastNameEntry.BorderBrush = Brushes.Red;
                valid = false;
            }
            if (!ValidateAlphabetical(CityEntry.Text))
            {
                CityEntry.BorderBrush = Brushes.Red;
                valid = false;
            }
            if (!ValidateNumerical(ZipEntry.Text) || ZipEntry.Text.Count() != ZipEntry.MaxLength)
            {
                ZipEntry.BorderBrush = Brushes.Red;
                valid = false;
            }
            bool isAccTypeSelected = RadioButtonParent.Children.OfType<RadioButton>().FirstOrDefault(x => x.IsChecked == true) != null;
            if (!isAccTypeSelected)
            {
                ShopRB.BorderBrush = System.Windows.Media.Brushes.Red;
                SellRB.BorderBrush = System.Windows.Media.Brushes.Red;
                valid = false;
            }

            if (!valid) return false; // returns before moving to actual validation

            User tmp = Users.FirstOrDefault(x => x.Username == UsernameEntry.Text);
            if (tmp != null)
            {
                UsernameEntry.BorderBrush = Brushes.Red;
                MessageBox.Show("Username already in use.", "Account Creation Error");
                return false;
            }
            if (PasswordEntry.Password != ConfirmPasswordEntry.Password)
            {
                PasswordEntry.BorderBrush = Brushes.Red;
                ConfirmPasswordEntry.BorderBrush = Brushes.Red;
                MessageBox.Show("Passwords do not match.", "Account Creation Error");
                return false;
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

        private bool ValidateAlphabetical(string tester)
        {
            return tester.Where(x => char.IsLetter(x) || char.IsSeparator(x)).Count() == tester.Length;
        }

        private bool ValidateNumerical(string tester)
        {
            return tester.Where(x => char.IsDigit(x)).Count() == tester.Length;
        }

        private void StateSelectorClicked(object sender, MouseButtonEventArgs e)
        {
            StateSelectorError.Visibility = Visibility.Hidden;
        }

        private void StateSelectorChanged(object sender, SelectionChangedEventArgs e)
        {
            StateSelectorError.Visibility = Visibility.Hidden;
        }

        private void PaymentClicked(object sender, MouseButtonEventArgs e)
        {
            PaymentError.Visibility = Visibility.Hidden;
        }

        private void PaymentChanged(object sender, SelectionChangedEventArgs e)
        {
            PaymentError.Visibility = Visibility.Hidden;
        }
        private void RbChecked(object sender, RoutedEventArgs e)
        {
            ShopRB.BorderBrush = System.Windows.Media.Brushes.DarkGray;
            SellRB.BorderBrush = System.Windows.Media.Brushes.DarkGray;
        }
    }
}
