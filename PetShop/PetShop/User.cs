using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PetShop
{
    [XmlRoot(ElementName = "User")]
    public class User
    {
        [XmlAttribute(DataType = "string")]
        public string FirstName { get; set; }

        [XmlAttribute(DataType = "string")]
        public string LastName { get; set; }

        [XmlAttribute(DataType = "string")]
        public string Username { get; set; }

        [XmlAttribute(DataType = "string")]
        public string Password { get; set; }

        [XmlAttribute(DataType = "string")]
        public string Email { get; set; }

        [XmlAttribute(DataType = "string")]
        public string Address { get; set; }

        [XmlAttribute(DataType = "string")]
        public string City { get; set; }

        [XmlAttribute(DataType = "string")]
        public string State { get; set; }

        [XmlAttribute(DataType = "int")]
        public int Zip { get; set; }

        [XmlAttribute(DataType = "string")]
        public string PaymentMethod { get; set; }

        [XmlAttribute]
        public bool Seller { get; set; }


        public User() {}



    }
}
