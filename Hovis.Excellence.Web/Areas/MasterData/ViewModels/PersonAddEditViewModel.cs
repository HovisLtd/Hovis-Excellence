using Hovis.Excellence.Web.Models;
using System.Collections.Generic;

namespace Hovis.Excellence.Web.Areas.MasterData.ViewModels
{
    public class PersonAddEditViewModel
    {
        public IEnumerable<PersonRole> AvailablePersonRoles { get; set; }

        public IEnumerable<Site> AvailableSites { get; set; }

        public Person Person { get; set; }
    }
}