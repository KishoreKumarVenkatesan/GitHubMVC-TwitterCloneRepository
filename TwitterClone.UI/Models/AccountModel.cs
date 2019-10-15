

namespace TwitterClone.UI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }
    public class LoginModel
        {
            [Required]
            [Display(Name = "UserName")]
            public string userId { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string password { get; set; }

           
        }

        public class SignupModel
        {
            [Required]
            [Display(Name = "UserId")]
            public string userId { get; set; }

        [Required]
        [Display(Name = "FullName")]
        public string fullName { get; set; }

        [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string password { get; set; }

        [Required]
        [Display(Name = "Joined")]
        public DateTime joined { get; set; }

        [Required]
        [Display(Name = "Active")]
        public  bool  active { get; set; }

    }

    public class ManageProfile
    {
        public string fullName { get; set; }
        public string password { get; set; }
        public string email { get; set; }

        public bool deleteAccount { get; set; }
    }

    public class FollowingUser
    {
        public string fullName { get; set; }
        [EmailAddress]
        public string email { get; set; }
    }

}
