using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Nest;
using System.Collections.Generic;

namespace SouthSideComics.Core.Models
{
    public class Item
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }        
        public string StockNumber { get; set; }
        [ElasticProperty(OptOut = true)]
        public string ParentItem { get; set; }        
        public string Title { get; set; }
        public string Description { get; set; }
        public string VariantDescription { get; set; }
        public Series Series { get; set; }
        [ElasticProperty(OptOut = true)]
        public string IssueNumber { get; set; }
        [ElasticProperty(OptOut = true)]
        public string IssueSequenceNumber { get; set; }
        [ElasticProperty(OptOut = true)]
        public string VolumeTag { get; set; }
        [ElasticProperty(OptOut = true)]
        public string MaxIssue { get; set; }
        [ElasticProperty(OptOut = true)]
        public string Price { get; set; }
        public Publisher Publisher { get; set; }
        public string UPCNumber { get; set; }
        public string ShortISBNNumber { get; set; }
        public string EANNumber { get; set; }
        [ElasticProperty(OptOut = true)]
        public string CardsPerPack { get; set; }
        [ElasticProperty(OptOut = true)]
        public string PackPerBox { get; set; }
        [ElasticProperty(OptOut = true)]
        public string BoxPerCase { get; set; }
        [ElasticProperty(OptOut = true)]
        public string DiscountCode { get; set; }
        [ElasticProperty(OptOut = true)]
        public string Increment { get; set; }
        [ElasticProperty(OptOut = true)]
        public string PrintDate { get; set; }
        [ElasticProperty(OptOut = true)]
        public string FOCVendor { get; set; }    
        public string ShipDate { get; set; }
        [ElasticProperty(OptOut = true)]
        public string StandardRetailPrice { get; set; }    
        public Category Category { get; set; }
        public Genre Genre { get; set; }
        [ElasticProperty(OptOut = true)]
        public string BrandCode { get; set; }
        public string Mature { get; set; }
        public string Adult { get; set; }
        [ElasticProperty(OptOut = true)]
        public string Caution1 { get; set; }
        [ElasticProperty(OptOut = true)]
        public string Caution2 { get; set; }
        [ElasticProperty(OptOut = true)]
        public string Caution3 { get; set; }        
        public PersonLink Writer { get; set; }
        public PersonLink Artist { get; set; }
        public PersonLink CoverArtist { get; set; }
        [ElasticProperty(OptOut = true)]
        public string AllianceSKU { get; set; }
        [ElasticProperty(OptOut = true)]
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
