using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace Dotnet.Url.Jumper.Application.Models
{
    public class Admin
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public bool Disabled { get; set; }
    }
}
