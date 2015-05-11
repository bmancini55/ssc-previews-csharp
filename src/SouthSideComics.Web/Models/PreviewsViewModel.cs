using SouthSideComics.Core.Common;
using SouthSideComics.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthSideComics.Web.Models
{
    public class PreviewsViewModel
    {
        public PagedList<PreviewsItem> Items { get; set; }
        public Dictionary<string, PreviewsCopy> Copy { get; set; }
    }
}
