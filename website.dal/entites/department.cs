using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace website.dal.entites
{
    public class department :baseentity
    {
        
        public string? name { get; set; }
        public string code { get; set; }
        public DateTime? createat { get; set; }
        public  List<employee>? employees { get; set; }

    }
}
