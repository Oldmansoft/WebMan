using Oldmansoft.Html.WebMan.Annotations;
using Oldmansoft.Html.WebMan.Input;
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
        [Required(ErrorMessage = "要填")]
        [StringLength(int.MaxValue, MinimumLength = 3)]
        [Description("请输入名称")]
        [RegularExpression(@"^[a-zA-Z0-9_\.]+$", ErrorMessage = "请使用字母和数字")]
        [ReadOnly(true)]
        public string Name { get; set; }

        [DataType(DataType.Password)]
        [ReadOnly(true)]
        public string Password { get; set; }

        [Display(Name = "内容")]
        [CustomInput(typeof(Markdown))]
        [ReadOnly(true)]
        public string Content { get; set; }

        [Display(Name = "状态")]
        [ReadOnly(true)]
        public DataTableItemState? State { get; set; }

        [Display(Name = "好吗")]
        [ReadOnly(true)]
        public bool? IsGood { get; set; }

        [Display(Name = "年龄")]
        [Range(1, 5)]
        [CustomInput(typeof(Select2))]
        [ReadOnly(true)]
        public int? Age { get; set; }

        [Display(Name = "列表")]
        [ReadOnly(true)]
        public List<DataTableItemState> States { get; set; }

        [Display(Name = "日期")]
        [DataType(DataType.Date)]
        [ReadOnly(true)]
        public DateTime Date { get; set; }

        [Display(Name = "时间")]
        [DataType(DataType.Time)]
        [ReadOnly(true)]
        public DateTime Time { get; set; }

        [Display(Name = "文件")]
        [FileOption(SupportDelete = true, Accept = ContentType.Image)]
        [ReadOnly(true)]
        public HttpPostedFileBase File { get; set; }

        [Display(Name = "文件组")]
        [FileOption(SupportDelete = true, Accept = ContentType.Image)]
        [HtmlData("lity")]
        [ReadOnly(true)]
        public List<HttpPostedFileBase> Files { get; set; }

        [CustomInput(typeof(TagsInput))]
        [ReadOnly(true)]
        public List<string> Tags { get; set; }

        [ReadOnly(true)]
        public double? DoubleValue { get; set; }

        [CustomInput(typeof(CustomInput.TestInput))]
        [ReadOnly(true)]
        public string HideValue { get; set; }

        [Display(Name = "创建")]
        [ReadOnly(true)]
        public DateTime? CreateTime { get; set; }
    }
}