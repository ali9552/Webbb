using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using website.dal.entites;

namespace website.pl.dtos.employeedtos
{
    public class createemployee
    {
        [Required(ErrorMessage ="name is req")]
        
        public string name { get; set; }
        [EmailAddress]
        public string email { get; set; }
        [Range(21, 50)]
        [Required]
        public int age { get; set; }
        [Required]
        public decimal salary { get; set; }
        [Required]
        public bool isactive { get; set; }
        [Required]
        public bool isdeleted { get; set; }
        [Required]
        public DateTime hiringdate { get; set; }
        [DisplayName("department")]
        public int? departmentid { get; set; }

        public IFormFile? image { get; set; }

        public string? imagename { get; set; }


    }
}
