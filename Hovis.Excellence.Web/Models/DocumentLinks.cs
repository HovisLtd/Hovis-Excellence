using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hovis.Excellence.Web.Models
{
    public class DocumentLinks : PersistedEntity
    {
        [Required]
        [DisplayName("Document Title")]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Owner { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a file")]
        public string FileLink { get; set; }

        [Required]
        public int DocID { get; set; }
    }
}