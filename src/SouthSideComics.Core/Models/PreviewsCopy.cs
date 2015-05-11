using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthSideComics.Core.Models
{
    public class PreviewsCopy
    {
        public int CopyId { get; set; }
        public string StockNumber { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string Preview { get; set; }
        public string Description { get; set; }
    }
}
