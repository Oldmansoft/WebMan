using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCore.Controllers
{
    public class FileController : Controller
    {
        public IActionResult Get(Guid id)
        {
            var data = DataService.Instance.Get(id);
            if (data == null) return NotFound();
            return File(data.Avatar.Content, data.Avatar.Type);
        }
    }
}
