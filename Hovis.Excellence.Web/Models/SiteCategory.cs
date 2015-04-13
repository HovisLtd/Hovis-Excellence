using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hovis.Excellence.Web.Models
{
    public class SiteCategory : PersistedEntity
    {
        public SiteCategory()
        {
            Sites = new List<Site>();
        }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Site> Sites { get; set; }
    }
}