using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
    /// Interaction logic for PlaceOrder.xaml
    /// </summary>
    public partial class PlaceOrder : Window
    {
        ShopperHomeVM WindowParent { get; set; }
        User Shopper;
        ObservableCollection<Pet> CartPets;
        List<Pet> PetList;
        XmlSerializer xmler = new XmlSerializer(typeof(List<Pet>));

        public PlaceOrder(ShopperHomeVM parent)
        {
            InitializeComponent();
            WindowParent = parent;
            Shopper = parent.Shopper;
            CartPets = parent.CartContents;
            PetList = parent.PetView.ItemsSource.Cast<Pet>().ToList();

            BillingName.Content = $"{Shopper.FirstName} {Shopper.LastName}";
            BillStreetAddr.Content = Shopper.Address;
            BillCityInfo.Content = $"{Shopper.City}, {Shopper.State} {Shopper.Zip}";

            ShippingName.Content = $"{Shopper.FirstName} {Shopper.LastName}";
            ShipStreetAddr.Content = Shopper.Address;
            ShipCityInfo.Content = $"{Shopper.City}, {Shopper.State} {Shopper.Zip}";

            double subtotal = 0;
            foreach(Pet pet in CartPets)
            {
                subtotal += pet.purchasedAmt * pet.Price;

                Label qty = new Label();
                qty.Content = pet.purchasedAmt;
                qty.HorizontalAlignment = HorizontalAlignment.Right;
                QtyStack.Children.Add(qty);

                Label name = new Label();
                name.Content = pet.Name;
                NameStack.Children.Add(name);

                Label price = new Label();
                price.Content = pet.Price;
                price.HorizontalAlignment = HorizontalAlignment.Right;
                PriceStack.Children.Add(price);

                Label total = new Label();
                total.Content = pet.total;
                total.HorizontalAlignment = HorizontalAlignment.Right;
                TotalStack.Children.Add(total);
            }

            double tax = subtotal * 0.07;
            double order_total = subtotal + tax;

            SubLabel.Content = string.Format("{0:N2}", subtotal);
            TaxLabel.Content = string.Format("{0:N2}", tax);
            TotalLabel.Content = "$" + string.Format("{0:N2}", order_total);
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EmailReceiptClick(object sender, RoutedEventArgs e)
        {
            WritePetsToDisk();
            EmailReceipt();
            System.Windows.Application.Current.Shutdown();
        }

        private void SaveReceiptClick(object sender, RoutedEventArgs e)
        {
            WritePetsToDisk();
            SaveReceipt();
            System.Windows.Application.Current.Shutdown();
        }

        private void WritePetsToDisk()
        {
            string path = "Pets.xml";

            using (FileStream filestream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                xmler.Serialize(filestream, PetList);
            }
        }


        //save and save as function using command parameters from the XAML to differentiate which action will occurprivate 
        void SaveReceipt()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Text Filex(*.txt)|*.txt";
            saveDialog.InitialDirectory = @"C:";
            if (saveDialog.ShowDialog() == true)
            {
                string fileText = $"Thank you {Shopper.FirstName} {Shopper.LastName} for your recent order " +
                $"from the Freshwater Snail Emporium." + System.Environment.NewLine +
                $"Total - {WindowParent.TotalCostToString}";
                string localSaveLocation = saveDialog.FileName;
                File.WriteAllText(localSaveLocation, fileText);
                MessageBox.Show("Receipt has been written to file", "Success");
            }
        }

        private void EmailReceipt()
        {
            string ServerEmail = "FreshwaterSnailEmporium@gmail.com";
            string ServerPass = "snails123";

            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Timeout = 10000;
            client.Credentials = new System.Net.NetworkCredential(ServerEmail, ServerPass);
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(ServerEmail);
            mail.To.Add(Shopper.Email);
            mail.Subject = "Order Confirmation - Freshwater Snail Emporium";
            mail.Body = $"Thank you {Shopper.FirstName} {Shopper.LastName} for your recent order " +
                $"from the Freshwater Snail Emporium."+ System.Environment.NewLine + 
                $"Total - {WindowParent.TotalCostToString}";
            mail.BodyEncoding = Encoding.UTF8;
            client.Send(mail);}
        }
}
