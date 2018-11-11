using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PetShop
{
    public class PetDisplayVM : INotifyPropertyChanged
    {
        XmlSerializer xmler = new XmlSerializer(typeof(List<Pet>));

        private ObservableCollection<Pet> pets;
        public ObservableCollection<Pet> Pets
        {
            get { return pets; }
            set { pets = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Pets"));
            }
        }


        public PetDisplayVM()
        {
            ReadInPets();
        }

        private Pet selectedPet;
        public Pet SelectedPet
        {
            get { return selectedPet; }
            set
            {
                selectedPet = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedDrink"));
            }
        }

        private void ReadInPets()
        {
            Pets = new ObservableCollection<Pet>()
            {
                new Pet("Asian Spiny Zebra Nerite", 13, 2.99, @"\Images\AsianSpinyZebraNerite.jpg"),
                new Pet("Bajingo Nerite", 4, 9.99, @"\Images\BajingoNerite.jpg"),
                new Pet("Green Tiger Nerite", 18, 4.99, @"\Images\GreenTigerNerite.jpg"),
                new Pet("Olive Nerite", 32, 1.29, @"\Images\OliveNerite.jpg"),
                new Pet("Red Lips Nerite", 22, 4.99, @"\Images\RedLipsNerite.jpg"),
                new Pet("Red Racer Nerite", 7,11.99, @"\Images\RedRacerNerite.jpg"),
                new Pet("Red Tiger Lady Nerite", 16, 4.99, @"\Images\RedTigerLadyNerite.jpg"),
                new Pet("Spiny Sun Nerite", 15, 3.99, @"\Images\SpinySunNerite.jpg")
            };

            //string path = "Pets.xml";
            //if (File.Exists(path))
            //{
            //    using (FileStream readStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            //    {
            //        Pets = xmler.Deserialize(readStream) as ObservableCollection<Pet>;
            //    }
            //}
        }

        //public static void UpdateQuantity(Pet snail, PetDisplayVM window)
        //{
        //    Pet petToUpdate = window.Pets.FirstOrDefault(x => x.Name == snail.Name);
        //    petToUpdate.Stock--;
        //    CollectionViewSource.GetDefaultView(window.Pets).Refresh();
        //}

        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
