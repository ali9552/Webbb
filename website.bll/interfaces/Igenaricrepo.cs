using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using website.dal.entites;

namespace website.bll.interfaces
{
    public interface Igenaricrepo<t> where t : baseentity 
    {
        Task< IEnumerable<t>> getall();

        Task< t >Get(int id);

        void update(t model);
        Task add(t model);
        void delete(t model);

    }
}
