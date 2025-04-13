using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using website.bll.interfaces;
using website.dal.context;

namespace website.bll.repositorys
{
    public class unitofwork : Iunitofwork
    {
        private readonly websitecontext _context;
        public Idepartmentrepo departmentrepo { get; }

        public Iemployeerepo employeerepo { get; }
        public unitofwork(websitecontext context)
        {
            _context = context;
            departmentrepo = new departmentrepo(_context);
            employeerepo = new Employeerepo(_context);
        }

       

        public async Task<int> complete()
        {
           return  await _context.SaveChangesAsync();
        }
    }
    
    }

