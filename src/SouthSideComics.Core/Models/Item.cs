using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace SouthSideComics.Core.Models
{
    public class Item
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }        
        public string StockNumber { get; set; }
        public string ParentItem { get; set; }        
        public string Title { get; set; }
        public string Description { get; set; }
        public string VariantDescription { get; set; }
        public Series Series { get; set; }
        public string IssueNumber { get; set; }
        public string IssueSequenceNumber { get; set; }
        public string VolumeTag { get; set; }
        public string MaxIssue { get; set; }
        public string Price { get; set; }
        public Publisher Publisher { get; set; }
        public string UPCNumber { get; set; }
        public string ShortISBNNumber { get; set; }
        public string EANNumber { get; set; }
        public string CardsPerPack { get; set; }
        public string PackPerBox { get; set; }
        public string BoxPerCase { get; set; }
        public string DiscountCode { get; set; }
        public string Increment { get; set; }
        public string PrintDate { get; set; }
        public string FOCVendor { get; set; }
        public string ShipDate { get; set; }
        public string StandardRetailPrice { get; set; }
        public Category Category { get; set; }
        public Genre Genre { get; set; }
        public string BrandCode { get; set; }
        public string Mature { get; set; }
        public string Adult { get; set; }        
        public string Caution1 { get; set; }
        public string Caution2 { get; set; }
        public string Caution3 { get; set; }        
        public PersonLink Writer { get; set; }
        public PersonLink Artist { get; set; }
        public PersonLink CoverArtist { get; set; }
        public string AllianceSKU { get; set; }
        public string FOCDate { get; set; }
        
        public List<PreviewsItemLink> Previews { get; set; }

        public class PreviewsItemLink
        {
            public PreviewsItemLink(PreviewsItem previewsItem)
            {
                if (previewsItem != null)
                {
                    PreviewNumber = previewsItem.DiamondNumber;
                    Page = previewsItem.Page;
                }
            }

            public string PreviewNumber { get; set; }            
            public string Page { get; set; }
        }

        public class PersonLink
        {
            public PersonLink(Person person)
            {
                if (person != null)
                {
                    Id = person.Id;
                    FullName = person.FullName;
                }
            }

            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; }
            public string FullName { get; set; }
        }
    }
}
