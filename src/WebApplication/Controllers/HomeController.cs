using Oldmansoft.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oldmansoft.Html.Mvc;
using Oldmansoft.Html.WebMan;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var document = new MainDocument("/Home/Welcome");
            document.Resources.AddScript(new Oldmansoft.Html.Element.ScriptResource(Url.Content("~/Scripts/oldmansoft-webman.cn.js")));
            document.Title = "WebMan";
            document.Menu.Add(new TreeListBranch(new LinkContent("欢迎", "/Home/Welcome", FontAwesome.Home)));
            document.Menu.Add(new TreeListBranch(new LinkContent("表格", "/Home/DataTables", FontAwesome.Tablet)));
            document.Taskbar.Add(new LinkContent(FontAwesome.Male));
            document.Taskbar.Add(new LinkContent(FontAwesome.Envelope));
            document.Account = new QuickMenu();
            document.Account.Image = "http://wx.qlogo.cn/mmopen/Q3auHgzwzM6SkLNbjL7Vq3koXuoq1PSv6Nlhp7AmfQjsB02hYG37blVecGupjK3GXm1iaYUKNh2z3PU8R6mo2HRTGfo066Fc2PCibT5B0asBo/0";
            document.Account.Text = "Oldman 老人";
            document.SetSearchAction("/Home/Search");
            return new HtmlResult(document);
        }

        [AllowAnonymous]
        public string Seed()
        {
            string seed = Guid.NewGuid().ToString().Substring(0, 4);
            TempData["HashSeed"] = seed;
            return seed;
        }

        public ActionResult Login()
        {
            var document = new LoginDocument("/Home/Seed", "/Home/Login");
            document.Resources.AddScript(new Oldmansoft.Html.Element.ScriptResource(Url.Content("~/Scripts/oldmansoft-webman.cn.js")));
            document.Title = "WebMan";
            return new HtmlResult(document);
        }

        [HttpPost]
        public JsonResult Login(Models.LoginModel model)
        {
            if (model.Account == "root" && !string.IsNullOrEmpty(model.Hash))
            {
                return Json(DealResult.Location("/"));
            }
            else
            {
                return Json(DealResult.CreateWrong("帐号或密码错误"));
            }
        }

        public ActionResult Welcome()
        {
            var panel = new Panel();
            panel.Caption = "hello";
            panel.Icon = FontAwesome.Anchor;
            var form = new FormHorizontal();
            form.Add("名称", new HtmlElement(HtmlTag.Input).AddClass("form-control").CreateGrid(Column.Sm9 | Column.Md10));
            form.Add("内容", new HtmlElement(HtmlTag.Input).AddClass("form-control").CreateGrid(Column.Sm9 | Column.Md10));
            panel.Append(form);

            return new HtmlResult(panel.CreateGrid());
        }

        public ActionResult DataTables()
        {
            var panel = new Panel();
            panel.Caption = "表格";
            panel.Icon = FontAwesome.Tablet;

            var table = DataTable.Definition<Models.DataTableItemModel>(o => o.Id, this.Location(DataTablesDataSource));
            table.AddTableAction("添加", "/Home/DataTablesCreate", LinkBehave.Open, false);
            table.AddTableAction("查看", "/Home/DataTablesView", LinkBehave.Link, true);
            table.AddTableAction("删除", "/Home/DataTablesDelete", LinkBehave.Call, true);
            table.AddItemAction("查看", "/Home/DataTablesView", LinkBehave.Open);
            table.AddItemAction("修改", "/Home/DataTablesCreate", LinkBehave.Link);
            table.AddItemAction("删除", "/Home/DataTablesDelete", LinkBehave.Call);
            panel.Append(table);
            return new HtmlResult(panel.CreateGrid());
        }

        public JsonResult DataTablesDataSource(DataTableRequest request)
        {
            System.Threading.Thread.Sleep(1000);
            var list = new List<Models.DataTableItemModel>();
            for (var i = 0; i < 10; i++)
            {
                var item = new Models.DataTableItemModel() { Name = "Hello", IsGood = true };
                item.States = new List<Models.DataTableItemState>();
                item.States.Add(Models.DataTableItemState.Low);
                item.States.Add(Models.DataTableItemState.Hight);
                item.Time = DateTime.Now;
                item.Date = DateTime.Now;
                item.CreateTime = DateTime.UtcNow;
                list.Add(item);
            }
            return Json(DataTable.Source(list, request, 100));
        }

        public ActionResult DataTablesCreate()
        {
            var model = new Models.DataTableItemModel();
            model.States = new List<Models.DataTableItemState>();
            model.States.Add(Models.DataTableItemState.Hight);

            var panel = new Panel();
            panel.Caption = "hello";
            panel.Icon = FontAwesome.Anchor;
            var form = FormHorizontal.Create(model, "/Home/DataTablesCreate");
            panel.Append(form);

            return new HtmlResult(panel.CreateGrid());
        }

        [HttpPost]
        public JsonResult DataTablesCreate(Models.DataTableItemModel model)
        {
            System.Threading.Thread.Sleep(1000);
            return Json(DealResult.Location("/Home/DataTables", "不能为空"));
        }

        public JsonResult DataTablesDelete(params Guid[] selectedId)
        {
            var output = new System.Text.StringBuilder();
            if (selectedId == null)
            {
                output.Append("null");
            }
            else
            {
                foreach (var item in selectedId)
                {
                    output.Append(item.ToString("N"));
                    output.Append(", ");
                }
            }

            return Json(new
            {
                Success = true,
                Message = output.ToString()
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DataTablesView(params Guid[] selectedId)
        {
            var panel = new Panel();
            panel.Caption = "hello";
            panel.Icon = FontAwesome.Anchor;
            foreach (var item in selectedId)
            {
                var div = new HtmlElement(HtmlTag.Div);
                div.Text(item.ToString("N"));
                panel.Append(div);
            }
            return new HtmlResult(panel.CreateGrid());
        }
    }
}