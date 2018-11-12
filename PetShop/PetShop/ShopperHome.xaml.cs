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

namespace PetShop
{
    /// <summary>
    /// Interaction logic for ShopperHome.xaml
    /// </summary>
    public partial class ShopperHome : Window
    {
        User Shopper;
        ListView PetView;
        public ShopperHome(ref User shopper)
        {
            InitializeComponent();
            PetView = MainPetDisplay.Content as ListView;
            ShopperHomeVM shopperWindow = new ShopperHomeVM(ref PetView, ref shopper);
            DataContext = shopperWindow;
            Shopper = shopper;
            ShopperLabel.Header = $"Logged in as: {Shopper.Username}";
            WelcomeLabel.Content = $"Welcome to the Freshwater Snail Emporium, {Shopper.FirstName}!";
        }

        private void ValidateQty(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Quantity.Text) || !ValidateNumerical(Quantity.Text))
            {
                Quantity.BorderBrush = Brushes.Red;
                return;
            }
            Pet selectedPet = PetView.SelectedItem as Pet;
            int purchasedAmount = int.Parse(Quantity.Text);
            if (purchasedAmount > selectedPet.Stock)
                Quantity.Text = selectedPet.Stock.ToString();
            if (selectedPet.Stock <= 0)
            {
                MessageBox.Show($"{selectedPet.Name} is sold out. You may have added the last one to your cart.", "Out of Stock");
            }
        }

        private bool ValidateNumerical(string tester)
        {
            return tester.Where(x => char.IsDigit(x)).Count() == tester.Length;
        }

        private void TextBoxChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            tb.BorderBrush = Brushes.DarkGray;
        }

        private void ExitCommand(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
