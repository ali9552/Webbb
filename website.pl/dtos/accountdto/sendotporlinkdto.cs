using System.ComponentModel.DataAnnotations;

namespace website.pl.dtos.accountdto
{
    public class sendotporlinkdto
    {
        [Required]
        [EmailAddress]
        public string? email { get; set; }
    }
}
