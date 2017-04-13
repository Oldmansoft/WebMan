using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class LoginModel
    {
        public string Account { get; set; }
        
        public string Hash { get; set; }
    }
}