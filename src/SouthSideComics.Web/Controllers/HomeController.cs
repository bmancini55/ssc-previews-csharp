using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using SouthSideComics.Core.Mappers;
using SouthSideComics.Core.Models;
using System.Threading.Tasks;
using SouthSideComics.Core.Common;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SouthSideComicsWeb.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ItemMapper itemMapper)
        {
            ItemMapper = itemMapper;
        }

        public ItemMapper ItemMapper { get; set; }

        public async Task<IActionResult> Index(int page = 1, int pagesize = 24)
        {
            var start = (page - 1) * pagesize;
            var model = await ItemMapper.FindAllAsync();
            model = new PagedList<Item>(model.Skip(start).Take(pagesize), page, pagesize, model.TotalCount);
            
            return View(model);
        }
    }
}
