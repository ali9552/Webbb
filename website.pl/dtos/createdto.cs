using System.ComponentModel.DataAnnotations;

namespace website.pl.dtos
{
    public class createdto
    {
        [Required(ErrorMessage ="code is req")]
        public string code { get; set; }
        [Required(ErrorMessage = "name is req")]
        public string name { get; set; }
        [Required(ErrorMessage = "creation date is req")]
        public DateTime createat { get; set; }

    }
}
