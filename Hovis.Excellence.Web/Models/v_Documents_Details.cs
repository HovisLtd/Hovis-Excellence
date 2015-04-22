using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hovis.Excellence.Web.Models
{
    public class v_Documents_Details : PersistedEntity
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

        [DisplayName("Application Id")]
        public int ApplicationId { get; set; }

        [DisplayName("Document Type Id")]
        public int DocumentTypeId { get; set; }

        [DisplayName("Document Cat Id")]
        public int DocumentCategoryId { get; set; }

        [DisplayName("Document Tab Id")]
        public int DocumentTabsId { get; set; }

        [DisplayName("Application Name")]
        public string ApplicationName { get; set; }

        [DisplayName("Document Type")]
        public string DocumentType { get; set; }

        [DisplayName("Document Category")]
        public string DocumentCategory { get; set; }

        [DisplayName("Document Tab")]
        public string DocumentTab { get; set; }

        [DisplayName("Review Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReviewDate { get; set; }
    }
}