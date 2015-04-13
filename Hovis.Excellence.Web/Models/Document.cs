using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hovis.Excellence.Web.Models
{
    public class Document : PersistedEntity
    {
        [Required]
        [DisplayName("Document Title")]
        public string Title { get; set; }

        [DisplayName("Issue Number")]
        public string IssueNumber { get; set; }

        [DisplayName("Issue Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime IssueDate { get; set; }

        public string Description { get; set; }

        public string Owner { get; set; }

        [DisplayName("Review Period (Months)")]
        public int ReviewPeriodInMonths { get; set; }

        [Required(ErrorMessage = "Please select an Application")]
        public int ApplicationId { get; set; }

        [DisplayName("Application")]
        [ForeignKey("ApplicationId")]
        public virtual HovisExcellenceApplication Applciation { get; set; }

        [Required(ErrorMessage = "Please select a Document Type")]
        public int DocumentTypeId { get; set; }

        [DisplayName("Document Type")]
        [ForeignKey("DocumentTypeId")]
        public virtual DocumentType DocumentType { get; set; }

        [Required(ErrorMessage = "Please select a Document Category")]
        public int DocumentCategoryId { get; set; }

        [DisplayName("Document Category")]
        [ForeignKey("DocumentCategoryId")]
        public virtual DocumentCategory DocumentCategory { get; set; }

        [Required(ErrorMessage = "Please select a Document Area")]
        public int DocumentTabsId { get; set; }

        [DisplayName("Document Area")]
        [ForeignKey("DocumentTabsId")]
        public virtual DocumentTabs DocumentTabs { get; set; }
    }
}