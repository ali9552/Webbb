using System.ComponentModel.DataAnnotations;

namespace website.pl.dtos
{
    public class editdto
    {
        [Required(ErrorMessage ="name req")]
        public string name { get; set; }
        [Required(ErrorMessage = "code req")]
        public string code { get; set; }
        [Required(ErrorMessage = "date req")]
        public DateTime? createat { get; set; }
    }
}
