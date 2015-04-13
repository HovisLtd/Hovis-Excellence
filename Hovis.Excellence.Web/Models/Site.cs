using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hovis.Excellence.Web.Models
{
    public class Site : PersistedEntity
    {
        [Required]
        public int SiteCategoryId { get; set; }

        [DisplayName("Category")]
        [ForeignKey("SiteCategoryId")]
        public virtual SiteCategory SiteCategory { get; set; }

        [DisplayName("Business Unit")]
        public string BusinessUnit { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public string RiskHealthManager { get; set; }

        [DisplayName("Utilyx Utility Site Code(s)")]
        public string UtilyxUtilitySiteCodes { get; set; }

        //these could be refactored later if there's going to be more "options" but for now holding them in the same table makes things nice and quick

        [DisplayName("Include In Board Report")]
        public bool IncludeInBoardReport { get; set; }

        [DisplayName("Miling Site")]
        public bool MillingSite { get; set; }

        [DisplayName("Divested Site")]
        public bool DivestedSite { get; set; }
    }
}