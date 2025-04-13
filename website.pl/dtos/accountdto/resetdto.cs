using System.ComponentModel.DataAnnotations;

namespace website.pl.dtos.accountdto
{
    public class resetdto
    {
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Required]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string confirmpassword { get; set; }

    }
}
