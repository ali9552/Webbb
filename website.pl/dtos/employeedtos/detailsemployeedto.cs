namespace website.pl.dtos.employeedtos
{
    public class detailsemployeedto
    {
        public int id { get; set; }
        public string name { get; set; }

        public string email { get; set; }

        public int age { get; set; }

        public decimal salary { get; set; }

        public bool isactive { get; set; }

        public bool isdeleted { get; set; }

        public DateTime hiringdate { get; set; }
        public int departmentid { get; set; }

       

    }
}
