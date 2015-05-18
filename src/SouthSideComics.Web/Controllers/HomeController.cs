using Microsoft.AspNet.Mvc;
using System.Threading.Tasks;
using SouthSideComics.Web.Models;
using SouthSideComics.Core.Mongo;
using SouthSideComics.Core.Common;
using SouthSideComics.Core.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SouthSideComicsWeb.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ItemMapper itemMapper, PublisherMapper publisherMapper, PersonMapper personMapper, SouthSideComics.Core.Elasticsearch.SearchItemMapper esItemMapper)
        {
            ItemMapper = itemMapper;
            PersonMapper = personMapper;
            PublisherMapper = publisherMapper;
            ESItemMapper = esItemMapper;
        }

        public ItemMapper ItemMapper { get; set; }
        public PublisherMapper PublisherMapper { get; set; }
        public PersonMapper PersonMapper { get; set; }
        public SouthSideComics.Core.Elasticsearch.SearchItemMapper ESItemMapper { get; set; }

        public async Task<IActionResult> Index(int page = 1, int pagesize = 24, string publisher = null, string writer = null, string artist = null, string query = null)
        {
            var publishers = await PublisherMapper.FindAllAsync(p => p.Name);
            var writers = await PersonMapper.FindWritersAsync();
            var artists = await PersonMapper.FindArtistsAsync();

            //var items = await ItemMapper.FindAsync(page, pagesize, "MAY15", publisher, writer, artist, p => p.Previews[0].PreviewNumber);
            var searchItems = await ESItemMapper.FindAsync(page, pagesize, "MAY15", publisher, writer, artist, query);
            var items = await ItemMapper.FindAsync(searchItems);

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
