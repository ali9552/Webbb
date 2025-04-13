using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using website.bll.interfaces;
using website.dal.context;
using website.dal.entites;

namespace website.bll.repositorys
{
    public class Employeerepo : genaricrepo<employee>, Iemployeerepo
    {

        private readonly websitecontext _context;
        public Employeerepo(websitecontext context) : base(context)
        {
            _context = context;

        }

        public async Task<IEnumerable<employee>> getbydepid(int? id)
        {
            return await _context.Set<employee>().Where(e => e.departmentid == id).ToListAsync();
        }

        public async Task<employee?> getbyemail(string Email)
        {
            return await _context.Set<employee>().FirstOrDefaultAsync(e=>e.email==Email);
        }

    }
}
