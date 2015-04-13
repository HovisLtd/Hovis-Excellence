using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hovis.Excellence.Web.Models
{
    public class Person : PersistedEntity
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        [DisplayName("Employee Number")]
        public string EmployeeNumber { get; set; }

        [Required]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual PersonRole Role { get; set; }

        [Required]
        public int SiteId { get; set; }

        [ForeignKey("SiteId")]
        public virtual Site Site { get; set; }

        public string Name { get { return string.Join(" ", FirstName, LastName); } }

        public bool IsSiteAdmin { get; set; }
    }
}