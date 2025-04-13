using System.Threading.Tasks;
using Azure.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using website.bll.interfaces;
using website.bll.repositorys;
using website.dal.entites;
using website.pl.dtos;

namespace website.pl.Controllers
{
    [Authorize]
    public class departmentController : Controller
    {
        private readonly Iunitofwork _unitofwork;

        
        public departmentController(Iunitofwork iunitofwork)
        {
            _unitofwork = iunitofwork;
        }
        public async Task<IActionResult> Index()
        {
            var department =await _unitofwork.departmentrepo.getall();

            return View(department);
        }
        [HttpGet]
        public IActionResult create()
        {

           

            return View();

            


        }
        [HttpPost]
         public async Task<IActionResult> create(createdto cdto) 
         {
            if (ModelState.IsValid)
            {
                var department = new department();
                department.code = cdto.code;
                department.name = cdto.name;
                department.createat = cdto.createat;

                 _unitofwork.departmentrepo.add(department);
                var count = await _unitofwork.complete();
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }

             return View(cdto);


         }
        [HttpGet]
        public async Task<IActionResult> details( int? id) 
        {

            var department =  await _unitofwork.departmentrepo.Get(id.Value);
            var detailsdto = new detailsdto();
            detailsdto.id = department.id;
            detailsdto.code = department.code;
            detailsdto.name = department.name;


            if (department == null)
            {
                return NotFound();
            }
            return View(detailsdto);


        }
        [HttpGet]
        public async Task<IActionResult> update(int? id)
        {
            var department = await _unitofwork.departmentrepo.Get(id.Value);
            var editdto = new editdto();
            editdto.createat = department.createat;
            editdto.code = department.code;
            editdto.name = department.name;

            if (department == null)
            {
                return NotFound();
            }
            return View(editdto);
            

            
        }
        [HttpPost]
        public async Task<IActionResult> update([FromRoute] int? id, editdto editdto)
        {
            var department = new department();
            department.id = id.Value;
            department.code = editdto.code;
            department.name = editdto.name;
            department.createat = editdto.createat.Value;


            if (ModelState.IsValid)
            {



                 _unitofwork.departmentrepo.update(department);
                var count = await _unitofwork.complete();
                if (count > 0)
                {    
                    return RedirectToAction("Index");
                }


            }


                return View(editdto);
        }
       
        public async Task<IActionResult> delete(int id)
        {
            var dep = await _unitofwork.departmentrepo.Get(id);
            if (dep == null)
            {
                return NotFound();
            }

            _unitofwork.departmentrepo.delete(dep);
            var count = await _unitofwork.complete();
            if (count > 0)
            {
                return RedirectToAction("Index");
            }

            return BadRequest();
        }
        public async Task<IActionResult> employees([FromRoute] int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var employees = await _unitofwork.employeerepo.getbydepid(id);

            var department = await _unitofwork.departmentrepo.Get(id.Value);
            ViewData["department"] = department.name;
            return View(employees);


        }

    }
}

