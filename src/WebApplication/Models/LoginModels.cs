using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class LoginModel
    {
        public string Account { get; set; }

        public string Password { get; set; }

        public string DoubleHash { get; set; }
    }
}