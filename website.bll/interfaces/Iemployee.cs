using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website.dal.entites;

namespace website.bll.interfaces
{
    public interface Iemployeerepo : Igenaricrepo<employee>
    {

       Task< employee >getbyemail(string email);
        Task<IEnumerable<employee>> getbydepid(int? id);
    }
}
