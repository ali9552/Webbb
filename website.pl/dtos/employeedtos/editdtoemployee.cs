using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace website.pl.dtos.employeedtos

{
    public class editdtoemployee
    {
        
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [EmailAddress]
        public string email { get; set; }
        [Range(21,50)]
        public int age { get; set; }

        public decimal salary { get; set; }

        public bool isactive { get; set; }

        public bool isdeleted { get; set; }

        public DateTime hiringdate { get; set; }
        public int departmentid { get; set; }
        public string? imagename { get; set; }
        public IFormFile? image { get; set; }

    }
}
