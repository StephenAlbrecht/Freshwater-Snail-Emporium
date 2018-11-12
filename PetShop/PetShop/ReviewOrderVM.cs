using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PetShop
{
    public class ReviewOrderVM : INotifyPropertyChanged
    {
        public ShopperHomeVM Parent { get; set; }
        public ReviewOrderVM(ShopperHomeVM parent) { Parent = parent; }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void UpdateCartClicked(object obj)
        {
            double sumTotal = 0;
            List<object> toDelete = new List<object>();

            foreach (Pet pet in Parent.CartContents)
            {
                Pet dbPet = Parent.PetView.ItemsSource.Cast<Pet>().FirstOrDefault(p => p.Name == pet.Name);
                if (pet.purchasedAmt > dbPet.purchasedAmt + dbPet.Stock)
                    pet.purchasedAmt = dbPet.purchasedAmt + dbPet.Stock;
                if (pet.purchasedAmt < 0) pet.purchasedAmt = 0;

                if (dbPet.purchasedAmt > pet.purchasedAmt)
                {
                    dbPet.Stock += dbPet.purchasedAmt - pet.purchasedAmt;
                    dbPet.purchasedAmt = pet.purchasedAmt; 
                }
                else
                {
                    dbPet.Stock -= pet.purchasedAmt - dbPet.purchasedAmt;
                    dbPet.purchasedAmt = pet.purchasedAmt; 
                }

                pet.total = "$" + (pet.Price * pet.purchasedAmt).ToString();
                sumTotal += pet.Price * pet.purchasedAmt;
                if (pet.purchasedAmt == 0) { toDelete.Add(pet); }
            }

            foreach (Pet petToRemove in toDelete) { Parent.CartContents.Remove(petToRemove); }
            Parent.TotalCost = sumTotal;
            Parent.TotalCostDisplay = $"Cart: {Parent.CartContents.Count()} items, ${string.Format("{0:N2}", Parent.TotalCost)}";
        }

        public ICommand UpdateCartCommand
        {
            get
            {
                if (_updateCartEvent == null)
                {
                    _updateCartEvent = new DelegateCommand(UpdateCartClicked);
                }
                return _updateCartEvent;
            }
        }
        DelegateCommand _updateCartEvent;
    }


}
