using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Nest;
using System.Collections.Generic;

namespace SouthSideComics.Core.Models
{
    public class SearchItem
    {
        public SearchItem(Item item)
        {
            if(item != null)
            {
                Id = item.Id;
                StockNumber = item.StockNumber;
                Title = item.Title;
                Description = item.Description;
                VariantDescription = item.VariantDescription;
                Series = item.Series;
                Publisher = item.Publisher;
                UPCNumber = item.UPCNumber;
                ShortISBNNumber = item.ShortISBNNumber;
                EANNumber = item.EANNumber;
                ShipDate = item.ShipDate;
                Category = item.Category;
                Genre = item.Genre;
                Mature = item.Mature;
                Adult = item.Adult;
                Writer = item.Writer;
                Artist = item.Artist;
                CoverArtist = item.CoverArtist;
                Previews = item.Previews;                     
            }
        }

        public string Id { get; set; }        
        public string StockNumber { get; set; }        
        public string Title { get; set; }
        public string Description { get; set; }
        public string VariantDescription { get; set; }
        public Series Series { get; set; }        
        public Publisher Publisher { get; set; }
        public string UPCNumber { get; set; }
        public string ShortISBNNumber { get; set; }
        public string EANNumber { get; set; }        
        public string ShipDate { get; set; }        
        public Category Category { get; set; }
        public Genre Genre { get; set; }        
        public string Mature { get; set; }
        public string Adult { get; set; }                
        public Item.PersonLink Writer { get; set; }
        public Item.PersonLink Artist { get; set; }
        public Item.PersonLink CoverArtist { get; set; }
        public List<Item.PreviewsItemLink> Previews { get; set; }
    }
}
