using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace PetShop
{
    class ShopperHomeVM : INotifyPropertyChanged
    {
        ListView PetView { get; set; }

        private double _totalCost = 0;
        public double TotalCost
        {
            get { return _totalCost; }
            set
            {
                _totalCost = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TotalCost"));
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

        public ShopperHomeVM(ref ListView petView)
        {
            PetView = petView;
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
            
            Pet tmp = CartContents.FirstOrDefault(p => p == selectedPet);
            if (tmp == null)
            {
                CartContents.Add(selectedPet);
            }
            else
            {
                CartContents.FirstOrDefault(p => p == selectedPet).purchasedAmt += purchasedAmount;
            }


            TotalCost += selectedPet.Price * purchasedAmount;
            TotalCostDisplay = $"Cart: {CartContents.Count()} items, ${string.Format("{0:N2}", TotalCost)}";
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

        private bool ValidateNumerical(string tester)
        {
            return tester.Where(x => char.IsDigit(x)).Count() == tester.Length;
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
