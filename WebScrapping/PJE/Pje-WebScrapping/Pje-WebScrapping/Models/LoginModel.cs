using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pje_WebScrapping.Models
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get;set; }
        public string Url { get; set; }
        public string? Access_Token { get; set;}
        public string? Name { get; set; }
        public string? OAB { get; set; }


        public LoginModel()
        {
        
        }
        public LoginModel(string username, string password, string url)
        {
            Username = username;
            Password = password;
            Url = url;
        }
    }
}
