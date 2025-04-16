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
    public class genaricrepo<t> : Igenaricrepo<t> where t : baseentity

        
        


    {

        private readonly websitecontext _context;

        public genaricrepo(websitecontext context) { _context = context; }
        public async Task add(t model)
        {
             await _context.Set<t>().AddAsync(model);
        }

        public void delete(t model)
        {
            if (typeof(t)==typeof(department))
            {
                var employees = _context.employees.Where(e => e.departmentid == model.id).ToList();
                foreach (var employee in employees)
                {
                    employee.isdeleted = true;
                    employee.isactive = false;
                    employee.departmentid = null;
                    _context.employees.Update(employee);
                }
            }
           

                _context.Set<t>().Remove(model);
        }

        public async Task<t> Get(int id)
        {
            return await _context.Set<t>().FindAsync(id);
        }

        public async Task<IEnumerable<t>> getall()
        {
            if (typeof(t) == typeof(employee))
            {
                var employees = await _context.employees.Include(e => e.department).ToListAsync();
                return (IEnumerable<t>)employees;
            }
            if (typeof(t) == typeof(department))
            {
                var departments = await _context.departments.Include(e => e.employees).ToListAsync();
                return (IEnumerable<t>)departments;
            }
            return await _context.Set<t>().ToListAsync();
        }
        public void update(t model)
        {
            _context.Set<t>().Update(model);
           
        }
    }
}
