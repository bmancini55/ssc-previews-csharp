using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using SouthSideComics.Core.Models;
using System.Threading.Tasks;
using SouthSideComics.Core.Common;
using SouthSideComics.Web.Models;
using SouthSideComics.Core.Mongo;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SouthSideComicsWeb.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(PreviewsItemMapper previewItemMapper)
        {            
            PreviewsItemMapper = previewItemMapper;
        }

        public PreviewsItemMapper PreviewsItemMapper { get; set; }        

        public async Task<IActionResult> Index(int page = 1, int pagesize = 24)
        {
            var start = (page - 1) * pagesize;
            var items = await PreviewsItemMapper.FindAllAsync(page, pagesize, p => p.DiamondNumber);
            var model = new PreviewsViewModel()
            {
                Items = items
            };

            return View(model);
        }
    }
}
