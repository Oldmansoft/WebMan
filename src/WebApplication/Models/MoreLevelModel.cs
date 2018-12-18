using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class MoreLevelModel
    {
        public string One { get; set; }

        [Oldmansoft.Html.WebMan.Annotations.Expansion]
        public TwoLevelModel ThreeLevel { get; set; }
    }

    public class ThirdLevelModel
    {
        public string Three { get; set; }
    }

    public class TwoLevelModel
    {
        public string Two { get; set; }

        [Oldmansoft.Html.WebMan.Annotations.Expansion]
        public ThirdLevelModel TwoLevel { get; set; }
    }
}