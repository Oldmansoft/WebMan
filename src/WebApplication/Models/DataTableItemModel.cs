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
        public string Name { get; set; }

        [Display(Name = "状态")]
        public DataTableItemState State { get; set; }

        [Display(Name = "好吗")]
        public bool? IsGood { get; set; }

        [Display(Name = "列表")]
        public List<DataTableItemState> States { get; set; }

        [Display(Name = "时间")]
        public DateTime CreatedTime { get; set; }

        [Display(Name = "动作")]
        public string Action { get; set; }
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