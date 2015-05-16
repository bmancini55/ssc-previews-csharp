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
        public HomeController(ItemMapper itemMapper, PublisherMapper publisherMapper, PersonMapper personMapper)
        {
            ItemMapper = itemMapper;
            PersonMapper = personMapper;
            PublisherMapper = publisherMapper;
        }

        public ItemMapper ItemMapper { get; set; }
        public PublisherMapper PublisherMapper { get; set; }
        public PersonMapper PersonMapper { get; set; }

        public async Task<IActionResult> Index(int page = 1, int pagesize = 24, string publisher = null, string writer = null, string artist = null)
        {
            var publishers = await PublisherMapper.FindAllAsync(p => p.Name);
            var writers = await PersonMapper.FindWritersAsync();
            var artists = await PersonMapper.FindArtistsAsync();
            var items = await ItemMapper.FindAsync(page, pagesize, "MAY15", publisher, writer, artist, p => p.Previews[0].PreviewNumber);
            var model = new HomeListModel()
            {
                Items = items,
                Publishers = publishers,
                Writers = writers,
                Artists = artists,
                Publisher = publisher,
                Writer = writer,
                Artist = artist
            };

            return View(model);
        }
    }
}
