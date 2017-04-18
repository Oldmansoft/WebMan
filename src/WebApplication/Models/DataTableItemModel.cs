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
        public Guid Id { get; set; }

        [Display(Name = "名称")]
        [Required]
        [StringLength(6)]
        [Description("请输入名称")]
        [RegularExpression(@"^[a-zA-Z0-9_\.]+$", ErrorMessage = "请使用字母和数字")]
        public string Name { get; set; }

        [Display(Name = "确认名称")]
        [Compare("Name", ErrorMessage = "输入和名称一样的值")]
        public string ConfirmName { get; set; }

        [Display(Name = "状态")]
        public DataTableItemState State { get; set; }

        [Display(Name = "好吗")]
        public bool? IsGood { get; set; }

        [Display(Name = "年龄")]
        public int? Age { get; set; }

        [Display(Name = "列表")]
        public List<DataTableItemState> States { get; set; }

        [Display(Name = "日期")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "时间")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        [Display(Name = "文件")]
        public HttpPostedFileBase File { get; set; }

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