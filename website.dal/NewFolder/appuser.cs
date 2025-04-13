using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Microsoft.AspNetCore.Identity;

namespace website.dal.NewFolder
{
    public class appuser :IdentityUser
    {

        public string firstname { get; set; }
        public string lastname { get; set; }
        public bool isagree { get; set; }



    }
}
