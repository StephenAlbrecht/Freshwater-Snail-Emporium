using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace PetShop
{
    class LoginDisplayVM
    {
        List<User> Users = new List<User>();
        XmlSerializer xmler = new XmlSerializer(typeof(List<User>));

        public LoginDisplayVM()
        {
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

        }

        private void LogInClicked(object obj)
        {

        }

        private void Autheticated()
        {

        }

        public ICommand CreateAccountCommand
        {
            get
            {
                if (_createAccountEvent == null)
                {
                    _createAccountEvent = new DelegateCommand(CreateAccountClicked);
                }
                return _createAccountEvent;
            }
        }
        DelegateCommand _createAccountEvent;

        public ICommand LogInCommand
        {
            get
            {
                if (_logInEvent == null)
                {
                    _logInEvent = new DelegateCommand(LogInClicked);
                }
                return _logInEvent;
            }
        }
        DelegateCommand _logInEvent;
    }
}
