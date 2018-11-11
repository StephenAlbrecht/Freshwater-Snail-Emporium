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

        public User(string firstName, string lastName, string username, string password, 
            string email, string address, string city, string state, int zip, 
            string paymentMethod, bool seller)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            Email = email;
            Address = address;
            City = city;
            State = state;
            Zip = zip;
            PaymentMethod = paymentMethod;
            Seller = seller;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName} ({Username})";
        }
    }

}
