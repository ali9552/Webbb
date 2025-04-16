using System.ComponentModel.DataAnnotations;

namespace website.pl.dtos.accountdto
{
    public class verifyemaildto
    {
        
        
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            
            [Display(Name = "OTP Code")]
            public string? OTP { get; set; }

       
    }
}
