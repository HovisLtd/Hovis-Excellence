using Hovis.Excellence.Web.Models;
using System.Collections.Generic;

namespace Hovis.Excellence.Web.Areas.MasterData.ViewModels
{
    public class DocumentListViewModel
    {
        public IEnumerable<Document> Documents { get; set; }

        public IEnumerable<HovisExcellenceApplication> Applications { get; set; }
    }
}