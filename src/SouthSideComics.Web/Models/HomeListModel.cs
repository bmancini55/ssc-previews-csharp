using Microsoft.AspNet.Mvc.Rendering;
using SouthSideComics.Core.Common;
using SouthSideComics.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthSideComics.Web.Models
{
    public class HomeListModel
    {
        public PagedList<Item> Items { get; set; }
        public PagedList<Publisher> Publishers { get; set; }
        public PagedList<Person> Writers { get; set; }
        public PagedList<Person> Artists { get; set; }       

        public string Publisher { get; set; }
        public string Writer { get; set; }
        public string Artist { get; set; }
        public string Query { get; set; }
        
        public IEnumerable<SelectListItem> PublisherList
        {
            get
            {
                return Publishers.Select(p => new SelectListItem() { Text = p.Name, Value = p.Id.ToString() });
            }
        }      

        public IEnumerable<SelectListItem> WritersList
        {
            get
            {
                return Writers.Select(p => new SelectListItem() { Text = p.FullName, Value = p.Id.ToString() });
            }
        }

        public IEnumerable<SelectListItem> ArtistsList
        {
            get
            {
                return Artists.Select(p => new SelectListItem() { Text = p.FullName, Value = p.Id.ToString() });
            }
        }
    }
}
