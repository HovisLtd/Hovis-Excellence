using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hovis.Excellence.Web.ViewModels
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        [RegularExpression(@".*@hovis.co.uk$", ErrorMessage = "Must be a @hovis.co.uk email address")]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public IEnumerable<string> SelectedRoles { get; set; }
    }
}