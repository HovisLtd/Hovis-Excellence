using Hovis.Excellence.Web.Models;
using System.Collections.Generic;

namespace Hovis.Excellence.Web.Areas.AppShared
{
    public class DocumentListViewModelcopy
    {

        public string DocumentCategory { get; set; }

        public IEnumerable<Document> Documents { get; set; }
    }
}