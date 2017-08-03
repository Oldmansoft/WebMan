using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class StepIndexModel
    {
        [Display(Name = "名称")]
        [Required]
        public string Name { get; set; }
    }

    public class StepNextModel
    {
        [Display(Name = "名称")]
        [ReadOnly(true)]
        public string Name { get; set; }

        [Display(Name = "内容")]
        [Oldmansoft.Html.WebMan.Annotations.CustomInput(typeof(Oldmansoft.Html.WebMan.Input.Markdown))]
        public string Content { get; set; }
    }

    public class StepFinishModel
    {
        [Display(Name = "名称")]
        [ReadOnly(true)]
        public string Name { get; set; }

        [Display(Name = "内容")]
        [Oldmansoft.Html.WebMan.Annotations.CustomInput(typeof(Oldmansoft.Html.WebMan.Input.Markdown))]
        [ReadOnly(true)]
        public string Content { get; set; }
    }
}