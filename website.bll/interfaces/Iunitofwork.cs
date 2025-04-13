using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace website.bll.interfaces
{
    public interface Iunitofwork
    {
        Idepartmentrepo departmentrepo { get; }
        Iemployeerepo employeerepo { get; }
       Task< int> complete();

    }
}
