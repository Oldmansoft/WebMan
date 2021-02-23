using Microsoft.AspNetCore.Mvc;
using Oldmansoft.Html.WebMan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCore.Controllers
{
    public class TableController : Controller
    {
        [Location("表格", Icon = FontAwesome.Table)]
        public IActionResult Index(string key)
        {
            var panel = new Panel();
            panel.ConfigLocation();

            var table = DataTable.Define<Models.TableListModel>(o => o.Id).Create(Url.Location(IndexDataSource));
            table.AddActionTable(Url.Location(Create));
            table.AddActionItem(Url.Location(Change));
            table.SetRowClassNameWhenClientCondition("alert-danger", "data.Id < 3");
            table.SetPageSize(20);
            panel.Append(table);
            panel.Description = "动态表格";
            panel.SetSearch(Url.Location(Index), "key", key, "示例");
            var result = new HtmlResult(panel.CreateGrid());
            result.SetQuickSearch(Url.Location(Index), "key", "示例");
            return result;
        }

        public JsonResult IndexDataSource(DataTable.Request request)
        {
            var list = new List<Models.TableListModel>();
            foreach(var item in DataService.Instance.List())
            {
                var model = new Models.TableListModel
                {
                    Id = item.Id,
                    Name = item.Name,
                };
                if (item.Avatar != null)
                {
                    model.Avatar = FileLocation.Create(item.Avatar.Name, item.Avatar.Type, string.Format("/Table/GetFile/{0}", item.Id));
                }
                list.Add(model);
            }
            return Json(DataTable.Source(list, request, list.Count));
        }

        [Location("创建", Behave = LinkBehave.Open)]
        public IActionResult Create()
        {
            var form = FormHorizontal.Create(new Models.TableEditModel(), Url.Location<Models.TableEditModel>(CreateResult));
            var panel = new Panel();
            panel.ConfigLocation();
            panel.Append(form);
            return new HtmlResult(panel);
        }

        public JsonResult CreateResult(Models.TableEditModel model)
        {
            DataService.Instance.Add(new Data() { Name = model.Name, Avatar = DataFile.Create(model.File) });
            return Json(DealResult.Refresh());
        }

        [Location("修改", Behave = LinkBehave.Open)]
        public IActionResult Change(Guid selectedId)
        {
            var data = DataService.Instance.Get(selectedId);
            if (data == null) return new HtmlResult();
            var model = new Models.TableEditModel()
            {
                Id = data.Id,
                Name = data.Name,
                File = FileLocation.Create(data.Avatar.Name, data.Avatar.Type, string.Format("/Table/GetFile/{0}", data.Id))
            };

            var form = FormHorizontal.Create(model, Url.Location<Models.TableEditModel>(ChangeResult));
            var panel = new Panel();
            panel.ConfigLocation();
            panel.Append(form);
            return new HtmlResult(panel);
        }

        public JsonResult ChangeResult(Models.TableEditModel model)
        {
            var data = DataService.Instance.Get(model.Id);
            if (data == null) return Json(DealResult.Refresh());
            model.DealUpload((file) =>
            {
                data.Avatar = DataFile.Create(file);
            }, Request.Form, () => {

            }, o => o.File);

            data.Name = model.Name;
            return Json(DealResult.Refresh());
        }

        public IActionResult GetFile(Guid id)
        {
            var data = DataService.Instance.Get(id);
            if (data == null) return new EmptyResult();
            if (data.Avatar == null) return new EmptyResult();
            return File(data.Avatar.Content, data.Avatar.Type);
        }
    }
}
