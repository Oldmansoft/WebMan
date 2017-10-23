using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class ShowModel
    {
        public int Id { get; set; }

        [Display(Name = "名称")]
        [ReadOnly(true)]
        public string Name { get; set; }
    }
}