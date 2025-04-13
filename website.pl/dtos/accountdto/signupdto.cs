using System.ComponentModel.DataAnnotations;

namespace website.pl.dtos.accountdto
{
    public class signupdto
    {
        [Required]
        public string firstname { get; set; }
        [Required]
        public string lastname { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 20 characters long.")]
        public string username { get; set; }
        [Required]
        [EmailAddress]
       
        public string email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        

        public string password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]

        public string confirmpassword { get; set; }
        [Required]
        

        public bool isagree { get; set; }

    }
}
