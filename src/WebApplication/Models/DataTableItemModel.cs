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
    public class DataTableItemModel
    {
        public int Id { get; set; }

        [Display(Name = "名称")]
        [Required(ErrorMessage = "要填")]
        [StringLength(int.MaxValue, MinimumLength = 3)]
        [Description("请输入名称")]
        [RegularExpression(@"^[a-zA-Z0-9_\.]+$", ErrorMessage = "请使用字母和数字")]
        public string Name { get; set; }
        
        [DataType(DataType.Password)]
        [ReadOnly(true)]
        public string Password { get; set; }

        [Display(Name = "内容")]
        [CustomInput(typeof(Markdown))]
        public string Content { get; set; }
        
        [Display(Name = "状态")]
        [Required]
        public DataTableItemState? State { get; set; }

        [Display(Name = "好吗")]
        public bool? IsGood { get; set; }

        [Display(Name = "年龄")]
        [Range(1, 5)]
        [CustomInput(typeof(Select2))]
        public int? Age { get; set; }

        [Display(Name = "列表")]
        [Required]
        public List<DataTableItemState> States { get; set; }

        [Display(Name = "日期")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "时间")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        [Display(Name = "文件")]
        [FileRequired]
        [FileOption(Accept = ContentType.Image)]
        public HttpPostedFileBase File { get; set; }
        
        [Display(Name = "文件组")]
        [FileRequired]
        [FileOption(SupportDelete = true, Accept = ContentType.Image, LimitContentLength = 1024 * 10)]
        [FixedCount(3)]
        [HtmlData("lity")]
        public List<HttpPostedFileBase> Files { get; set; }
        
        [CustomInput(typeof(TagsInput))]
        [Required]
        public List<string> Tags { get; set; }
        
        public double? DoubleValue { get; set; }

        [CustomInput(typeof(CustomInput.TestInput))]
        public string HideValue { get; set; }
        
        [Display(Name = "创建")]
        public DateTime? CreateTime { get; set; }
    }

    public enum DataTableItemState
    {
        [Description("默认")]
        Default,
        [Description("低")]
        Low,
        [Description("高")]
        Hight
    }
}