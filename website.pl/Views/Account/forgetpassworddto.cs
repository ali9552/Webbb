using System.ComponentModel.DataAnnotations;

namespace website.pl.Views.Account
{
    public class forgetpassworddto
    {
        [Required]
        [EmailAddress]

        public string email { get; set; }

    }
}
