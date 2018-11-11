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
    public class AddUserDisplayVM
    {
        List<User> Users = new List<User>();
        XmlSerializer xmler = new XmlSerializer(typeof(List<User>));
        List<string> States = new List<string>
        {
            "AK", "AL", "AR", "AZ", "CA", "CO", "CT", "DC", "DE", "FL",
            "GA", "HI", "IA", "ID", "IL", "IN", "KS", "KY", "LA", "MA",
            "MD", "ME", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE",
            "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "RI",
            "SC", "SD", "TN", "TX", "UT", "VA", "VT", "WA", "WI", "WV", "WY"
        };

        public AddUserDisplayVM(ref List<User> users)
        {
            Users = users;
            
        }

        private void CreateAccountClicked(object obj)
        {
            if (Authenticated())
            {
                User newUser = new User();
                Users.Add(newUser);
                WriteUsersToFile();
            }
        }

        private void CancelClicked(object obj)
        {
            
        }

        private void WriteUsersToFile()
        {
            string path = "Users.xml";

            if (Users.Count == 0 && File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                using (FileStream filestream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
                {
                    xmler.Serialize(filestream, Users);
                }
            }
        }

        private bool Authenticated()
        {
            return true;
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
                if (_cancelEvent == null)
                {
                    _cancelEvent = new DelegateCommand(CancelClicked);
                }
                return _cancelEvent;
            }
        }
        DelegateCommand _cancelEvent;
    }
}
