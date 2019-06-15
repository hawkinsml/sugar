using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sugar.Components.Commands
{ 
    class PasswordsModel
    {
        public PasswordsModel()
        {
            Passwords = new List<PasswordModel>();
        }

        public List<PasswordModel> Passwords { get; set; }
    }


    class PasswordModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
