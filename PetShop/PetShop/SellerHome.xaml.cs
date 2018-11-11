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
    /// Interaction logic for SellerHome.xaml
    /// </summary>
    public partial class SellerHome : Window
    {
        User Seller;
        public SellerHome(ref User seller)
        {
            InitializeComponent();
            Seller = seller;
            WelcomeLabel.Content = $"Welcome back, {Seller.FirstName}!";
        }
    }
}
