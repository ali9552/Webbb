using System.ComponentModel.DataAnnotations;

namespace website.pl.dtos.accountdto
{
    public class logindto
    {
        

        
        [Required]
        

        public string emailorusername { get; set; }
        [Required]

        [DataType(DataType.Password)]
        public string password { get; set; }

    }
}
