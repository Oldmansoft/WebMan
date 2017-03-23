using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class DataTableItemModel
    {
        public Guid Id { get; set; }

        [Display(Name = "名称")]
        public string Name { get; set; }

        [Display(Name = "时间")]
        public DateTime CreatedTime { get; set; }

        [Display(Name = "操作")]
        public string Action { get; set; }
    }
}