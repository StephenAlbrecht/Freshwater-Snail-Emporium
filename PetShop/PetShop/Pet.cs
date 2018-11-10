using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PetShop
{
    [XmlRoot(ElementName = "Pet")]
    public class Pet
    {
        [XmlAttribute(DataType = "string")]
        public string Name { get; set; }
        [XmlAttribute(DataType = "int")]
        public int Stock { get; set; }
        [XmlAttribute(DataType = "double")]
        public double Price { get; set; }
        [XmlAttribute(DataType = "string")]
        public string ImagePath { get; set; }

        public Pet() {}

        public Pet(string name, int stock, double price, string imagePath)
        {
            Name = name;
            Stock = stock;
            Price = price;
            ImagePath = imagePath;
        }
    }
}
