using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace website.dal.entites
{
    public class employee : baseentity
    {

        public string name { get; set; }

        public string email { get; set; }

        public int age { get; set; }

        public decimal salary { get; set; }

        public bool isactive { get; set; }

        public bool isdeleted { get; set; }

        public DateTime hiringdate { get; set; }
        public department? department { get; set; }
        public int? departmentid { get; set; }
        public string? imagename { get; set; }




    }
}
