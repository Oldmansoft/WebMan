using Oldmansoft.ClassicDomain.Util;
using Oldmansoft.Html.WebMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class DataTablesItemController : Controller
    {

        private static Dictionary<int, List<Models.DataTableItemModel>> DataSource { get; set; }

        private static List<Models.DataTableItemModel> GetDataSource(int id)
        {
            if (DataSource == null)
            {
                DataSource = new Dictionary<int, List<Models.DataTableItemModel>>();
            }
            if (DataSource.ContainsKey(id)) return DataSource[id];

            var list = new List<Models.DataTableItemModel>();
            DataSource.Add(id, list);
            return list;
        }

        [Location("表格", Icon = FontAwesome.Tablet, Behave = LinkBehave.Open)]
        public ActionResult Index(int selectedId)
        {
            var panel = new Panel();
            panel.ConfigLocation();

            var table = DataTable.Definition<Models.DataTableItemModel>(o => o.Id).Create(GetDataSource(selectedId));
            table.AddActionTable(Url.Location(Create).Set("parentId", selectedId));
            table.AddActionTable(Url.Location(new Func<int, int[], JsonResult>(Delete)).Set("parentId", selectedId)).SupportParameter().Confirm("是否删除").NeedSelected();
            table.AddActionItem(Url.Location(new Func<int, int, ActionResult>(Details)).Set("parentId", selectedId));
            table.AddActionItem(Url.Location(new Func<int, int, ActionResult>(Edit)).Set("parentId", selectedId));
            table.AddActionItem(Url.Location(new Func<int, int[], JsonResult>(Delete)).Set("parentId", selectedId)).Confirm("是否删除");
            panel.Append(table);
            return new HtmlResult(panel.CreateGrid());
        }
        
        [Location("添加", Icon = FontAwesome.Anchor, Behave = LinkBehave.Open)]
        public ActionResult Create(int parentId)
        {
            var model = new Models.DataTableItemModel();
            model.States = new List<Models.DataTableItemState>();
            model.States.Add(Models.DataTableItemState.Hight);
            model.Time = DateTime.Now;
            model.Date = DateTime.Now;
            model.CreateTime = DateTime.UtcNow;

            var source = new ListDataSource();
            source["Age"].Add(new ListDataItem("1", "1"));
            source["Age"].Add(new ListDataItem("2", "2"));

            var panel = new Panel();
            panel.ConfigLocation();
            var form = FormHorizontal.Create(model, Url.Location(new Func<int, Models.DataTableItemModel, JsonResult>(Create)).Set("parentId", parentId), source);
            panel.Append(form);

            return new HtmlResult(panel);
        }

        [HttpPost]
        public JsonResult Create(int parentId, Models.DataTableItemModel model)
        {
            if (ModelState.ValidateFail())
            {
                return Json(DealResult.Wrong(ModelState.ValidateMessage()));
            }
            if (GetDataSource(parentId).Count == 0)
            {
                model.Id = 1;
            }
            else
            {
                model.Id = GetDataSource(parentId).Max(o => o.Id) + 1;
            }
            GetDataSource(parentId).Insert(0, model);
            return Json(DealResult.Refresh("添加成功"));
        }

        [Location("修改", Icon = FontAwesome.Anchor)]
        public ActionResult Edit(int parentId, int selectedId)
        {
            var model = GetDataSource(parentId).FirstOrDefault(o => o.Id == selectedId);
            var source = new ListDataSource();
            source["Age"].Add(new ListDataItem("1", "1"));
            source["Age"].Add(new ListDataItem("2", "2"));

            var panel = new Panel();
            panel.ConfigLocation();
            var form = FormHorizontal.Create(model, Url.Location(new Func<int, Models.DataTableItemModel, JsonResult>(Edit)).Set("parentId", parentId), source);
            panel.Append(form);

            return new HtmlResult(panel.CreateGrid());
        }

        [HttpPost]
        public JsonResult Edit(int parentId, Models.DataTableItemModel model)
        {
            if (ModelState.ValidateFail())
            {
                return Json(DealResult.Wrong(ModelState.ValidateMessage()));
            }
            var data = GetDataSource(parentId).FirstOrDefault(o => o.Id == model.Id);
            if (data != null)
            {
                model.CopyTo(data);
            }

            return Json(DealResult.Refresh("修改成功"));
        }

        [Location("删除")]
        public JsonResult Delete(int parentId, params int[] selectedId)
        {
            foreach (var id in selectedId)
            {
                var model = GetDataSource(parentId).FirstOrDefault(o => o.Id == id);
                if (model == null) return Json(DealResult.Wrong("没有删除项"), JsonRequestBehavior.AllowGet);
                GetDataSource(parentId).Remove(model);
            }

            return Json(DealResult.Refresh("删除成功"), JsonRequestBehavior.AllowGet);
        }

        [Location("详情", Icon = FontAwesome.Anchor, Behave = LinkBehave.Open)]
        public ActionResult Details(int parentId, int selectedId)
        {
            var model = GetDataSource(parentId).FirstOrDefault(o => o.Id == selectedId);

            var panel = new Panel();
            panel.ConfigLocation();
            var form = FormHorizontal.Create(model);
            panel.Append(form);

            return new HtmlResult(panel);
        }
    }
}