using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PetShop
{
    public class ShopperHomeVM : INotifyPropertyChanged
    {
        public ListView PetView { get; set; }
        public User Shopper;

        private double _totalCost = 0;
        public double TotalCost
        {
            get { return _totalCost; }
            set
            {
                _totalCost = value;
                TotalCostToString = "$" + string.Format("{0:N2}", _totalCost);
                PropertyChanged(this, new PropertyChangedEventArgs("TotalCost"));
            }
        }

        private string _totalCostToString = "$0.00";
        public string TotalCostToString
        {
            get { return _totalCostToString; }
            set
            {
                _totalCostToString = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TotalCostToString"));
            }
        }

        private string totalCostdisplay = "Cart: 0 items, $0.00" ;
        public string TotalCostDisplay
        {
            get { return totalCostdisplay; }
            set
            {
                totalCostdisplay = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TotalCostDisplay"));
            }
        }

        private string _quantity;
        public string Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Quantity"));
            }
        }

        private ObservableCollection<Pet> _cartContents = new ObservableCollection<Pet>();
        public ObservableCollection<Pet> CartContents
        {
            get { return _cartContents; }
            set
            {
                _cartContents = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CartContents"));
            }
        }

        public ShopperHomeVM(ref ListView petView, ref User shopper)
        {
            PetView = petView;
            Shopper = shopper;
        }

        private void AddToCartClicked(object obj)
        {
            // validate qty, if > stock, set to stock
            if(string.IsNullOrWhiteSpace(Quantity) || !ValidateNumerical(Quantity))
            {
                return;
            }
            Pet selectedPet = PetView.SelectedItem as Pet;
            int purchasedAmount = int.Parse(Quantity);
            if (purchasedAmount > selectedPet.Stock)
                purchasedAmount = selectedPet.Stock;
            
            selectedPet.Stock -= purchasedAmount;
            selectedPet.purchasedAmt += purchasedAmount;
            Pet tmp = CartContents.FirstOrDefault(p => p.Name == selectedPet.Name);
            if (tmp == null)
            {
                Pet cartPet = new Pet(selectedPet.Name, selectedPet.Stock, selectedPet.Price, selectedPet.ImagePath);
                cartPet.purchasedAmt = purchasedAmount;
                cartPet.total = "$"+(cartPet.purchasedAmt * cartPet.Price).ToString();
                CartContents.Add(cartPet);
            }
            else
            {
                tmp.purchasedAmt += purchasedAmount;
                tmp.total = "$" + (tmp.purchasedAmt * tmp.Price).ToString();
            }

            //MessageBox.Show($"{purchasedAmount} {selectedPet.Name} added to cart.", "Pet Added to Cart");
            TotalCost += selectedPet.Price * purchasedAmount;
            TotalCostDisplay = $"Cart: {CartContents.Count()} items, ${string.Format("{0:N2}", TotalCost)}";
        }

        private void ReviewCartClicked(object obj)
        {
            ReviewOrderVM reviewWindow = new ReviewOrderVM(this);
            ReviewOrder cart = new ReviewOrder();
            cart.DataContext = reviewWindow;
            cart.Show();
        }

        private void PlaceOrderClicked(object obj)
        {
            PlaceOrder orderWindow = new PlaceOrder(this);
            orderWindow.ShowDialog();
        }

        public ICommand AddToCartCommand
        {
            get
            {
                if (_addToCartEvent == null)
                {
                    _addToCartEvent = new DelegateCommand(AddToCartClicked);
                }
                return _addToCartEvent;
            }
        }
        DelegateCommand _addToCartEvent;

        public ICommand ReviewOrderCommand
        {
            get
            {
                if (_reviewCartEvent == null)
                {
                    _reviewCartEvent = new DelegateCommand(ReviewCartClicked);
                }
                return _reviewCartEvent;
            }
        }
        DelegateCommand _reviewCartEvent;

        public ICommand PlaceOrderCommand
        {
            get
            {
                if (_placeOrderClicked == null)
                {
                    _placeOrderClicked = new DelegateCommand(PlaceOrderClicked);
                }
                return _placeOrderClicked;
            }
        }
        DelegateCommand _placeOrderClicked;

        private bool ValidateNumerical(string tester)
        {
            return tester.Where(x => char.IsDigit(x)).Count() == tester.Length;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
