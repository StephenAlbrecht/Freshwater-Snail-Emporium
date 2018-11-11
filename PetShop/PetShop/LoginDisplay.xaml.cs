using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace PetShop
{
    /// <summary>
    /// Interaction logic for LoginDisplay.xaml
    /// </summary>
    public partial class LoginDisplay : UserControl
    {
        List<User> Users = new List<User>();
        XmlSerializer xmler = new XmlSerializer(typeof(List<User>));

        public LoginDisplay()
        {
            InitializeComponent();
            ReadInUsers();
        }

        private void ReadInUsers()
        {
            string path = "Users.xml";
            if (File.Exists(path))
            {
                using (FileStream readStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    Users = xmler.Deserialize(readStream) as List<User>;
                }
            }
        }

        private void CreateAccountClicked(object obj)
        {
            if (Authenticated())
            {
                AddUser addUserWindow = new AddUser(ref Users);
                addUserWindow.ShowDialog();
            }
        }

        private void LogInClicked(object obj)
        {

        }

        private bool Authenticated()
        {
            return true;
        }
    }
}
