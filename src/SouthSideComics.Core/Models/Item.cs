using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthSideComics.Core.Models
{
    public class Item
    {
        /// <summary>
        /// Surrogate key
        /// </summary>
        public int ItemId { get; set; }
        public string DiamondNumber { get; set; }
        public string StockNumber { get; set; }
        public string ParentItem { get; set; }
        public string BounceUseItem { get; set; }
        public string FullTitle { get; set; }
        public string MainDescription { get; set; }
        public string VariantDescription { get; set; }
        public string SeriesCode { get; set; }
        public string IssueNumber { get; set; }
        public string IssueSequenceNumber { get; set; }
        public string VolumeTag { get; set; }
        public string MaxIssue { get; set; }
        public string Price { get; set; }
        public string Publisher { get; set; }
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
        public string Category { get; set; }
        public string Genre { get; set; }
        public string BrandCode { get; set; }
        public string Mature { get; set; }
        public string Adult { get; set; }
        public string OfferedAgain { get; set; }
        public string Caution1 { get; set; }
        public string Caution2 { get; set; }
        public string Caution3 { get; set; }
        public string Resolicited { get; set; }
        public string NotePrice { get; set; }
        public string OrderFormNotes { get; set; }
        public string Page { get; set; }
        public string Writer { get; set; }
        public string Artist { get; set; }
        public string CoverArtist { get; set; }
        public string AllianceSKU { get; set; }
        public string FOCDate { get; set; }
    }
}
