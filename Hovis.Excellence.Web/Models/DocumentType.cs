using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hovis.Excellence.Web.Models
{
    public class DocumentType : PersistedEntity
    {
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}