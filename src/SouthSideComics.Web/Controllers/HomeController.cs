using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using SouthSideComics.Core.Models;
using System.Threading.Tasks;
using SouthSideComics.Core.Common;
using SouthSideComics.Web.Models;
using SouthSideComics.Core.MySql;
using SouthSideComics.Core.Mongo;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SouthSideComicsWeb.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(SouthSideComics.Core.MySql.PreviewsItemMapper mysqlItemMapper, SouthSideComics.Core.MySql.PreviewsCopyMapper mysqlCopyMapper, SouthSideComics.Core.Mongo.PreviewsItemMapper mongoItemMapper)
        {
            MySqlItemMapper = mysqlItemMapper;
            MySqlCopyMapper = mysqlCopyMapper;
            MongoItemMapper = mongoItemMapper;
        }

        public SouthSideComics.Core.MySql.PreviewsItemMapper MySqlItemMapper { get; set; }
        public SouthSideComics.Core.MySql.PreviewsCopyMapper MySqlCopyMapper { get; set; }
        public SouthSideComics.Core.Mongo.PreviewsItemMapper MongoItemMapper { get; set; }

        public async Task<IActionResult> MySql(int page = 1, int pagesize = 24)
        {
            var start = (page - 1) * pagesize;
            var items = await MySqlItemMapper.FindAllAsync();
            items = new PagedList<PreviewsItem>(items.Skip(start).Take(pagesize), page, pagesize, items.TotalCount);
        
            var tasks = items.Select(p => MySqlCopyMapper.FindByStockNumberAsync(p.DiamondNumber));
            var copy = await Task.WhenAll(tasks);
            var copyJoin = copy.ToDictionary(p => p.StockNumber);

            var model = new PreviewsViewModel()
            {
                Items = items,
                Copy = copyJoin
            };
            
            return View(model);
        }

        public async Task<IActionResult> Mongo(int page = 1, int pagesize = 24)
        {
            var start = (page - 1) * pagesize;
            var items = await MongoItemMapper.FindAllAsync();
            items = new PagedList<PreviewsItem>(items.Skip(start).Take(pagesize), page, pagesize, items.TotalCount);
            var model = new PreviewsViewModel()
            {
                Items = items
            };

            return View(model);
        }
    }
}
