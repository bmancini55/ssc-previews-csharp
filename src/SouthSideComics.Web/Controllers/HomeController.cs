using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using SouthSideComics.Core.Mappers;
using SouthSideComics.Core.Models;
using System.Threading.Tasks;
using SouthSideComics.Core.Common;
using SouthSideComics.Web.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SouthSideComicsWeb.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(PreviewsItemMapper itemMapper, PreviewsCopyMapper copyMapper)
        {
            ItemMapper = itemMapper;
            CopyMapper = copyMapper;
        }

        public PreviewsItemMapper ItemMapper { get; set; }
        public PreviewsCopyMapper CopyMapper { get; set; }

        public async Task<IActionResult> Index(int page = 1, int pagesize = 24)
        {
            var start = (page - 1) * pagesize;
            var items = await ItemMapper.FindAllAsync();
            items = new PagedList<PreviewsItem>(items.Skip(start).Take(pagesize), page, pagesize, items.TotalCount);
        
            var tasks = items.Select(p => CopyMapper.FindByStockNumberAsync(p.StockNumber));
            var copy = await Task.WhenAll(tasks);
            var copyJoin = copy.ToDictionary(p => p.StockNumber);

            var model = new PreviewsViewModel()
            {
                Items = items,
                Copy = copyJoin
            };
            
            return View(model);
        }
    }
}
