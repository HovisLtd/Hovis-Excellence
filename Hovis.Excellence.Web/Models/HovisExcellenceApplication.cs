using System.ComponentModel.DataAnnotations;

namespace Hovis.Excellence.Web.Models
{
    public class HovisExcellenceApplication : PersistedEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ApplicationKey { get; set; }
    }
}