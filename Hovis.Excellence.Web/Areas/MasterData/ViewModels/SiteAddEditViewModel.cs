using System.Collections.Generic;
using Hovis.Excellence.Web.Models;

namespace Hovis.Excellence.Web.Areas.MasterData.ViewModels
{
    public class SiteAddEditViewModel
    {
        public IEnumerable<SiteCategory> AvailableSiteCategories { get; set; }

        public Site Site { get; set; }
    }
}