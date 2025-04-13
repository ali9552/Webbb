using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website.bll.interfaces;
using website.dal.context;
using website.dal.entites;

namespace website.bll.repositorys
{
    public class departmentrepo : genaricrepo<department> , Idepartmentrepo
    {
        public departmentrepo(websitecontext context) : base(context)
        {

        }


    }
}
