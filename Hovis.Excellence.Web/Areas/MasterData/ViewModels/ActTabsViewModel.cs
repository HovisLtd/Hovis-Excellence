using Hovis.Excellence.Web.Models;
using System.Collections.Generic;

namespace Hovis.Excellence.Web.Areas.MasterData.ViewModels
{
    public class ActTabsViewModel
    {
        public class DocumentGroupList
        {
            public string Name { get; set; }

            public List<Document> Documents { get; set; }
        }

        public IEnumerable<DocumentGroupList> ByTabs { get; set; }

        public IEnumerable<DocumentGroupList> ByCats { get; set; }
    }

    public class ActTabsViewModelV2
    {
        public class DocumentGroupList
        {
            public string TabName { get; set; }

            public string CatName { get; set; }

            public List<Document> Documents { get; set; }
        }

        public IEnumerable<DocumentGroupList> ByTabsAndCat { get; set; }
     }

    public class ActTabsViewModelV3
    {
        public class Group1
        {
            public string CatName { get; set; }

            public List<Document> Documents { get; set; }
        }

        public class DocumentGroupList
        {
            public string TabName { get; set; }

            public virtual Group1 Documents { get; set; }
        }

        public IEnumerable<DocumentGroupList> ByTabsAndCat { get; set; }
    }

    public class ActTabsViewModelV4
    {
         public string TabName { get; set; }
    }

    public class ActTabsViewModelV5
    {
        public string CatName { get; set; }
    }

    public class ActTabsViewModelV6
    {
        public string TabName { get; set; }

        public string CatName { get; set; }
    }
}