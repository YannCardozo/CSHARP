using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.data
{
    public class LoginDetails
    {

        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public LoginDetails(string login, string email, string password)
        {
            Login = login;
            Email = email;
            Password = password;
        }

        //dados referentes ao WATTPAD
        public LoginDetails()
        {
            Login = "UsernameTeste";
            Email = "yann.cardozo@soulasalle.com.br";
            Password = "Chaons26196460!@";
        }







    }
}
