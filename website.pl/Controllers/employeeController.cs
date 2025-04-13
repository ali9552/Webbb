using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using website.bll.interfaces;
using website.bll.repositorys;
using website.dal.entites;
using website.pl.dtos.employeedtos;
using website.pl.helpers;

namespace website.pl.Controllers
{
    [Authorize]
    public class employeeController : Controller
    {
        private readonly Iunitofwork _unitofwork;
        private readonly IMapper _mapper;
        public employeeController(Iunitofwork unitofwork,IMapper map)
        {
            _unitofwork = unitofwork;
            _mapper = map;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                var employees = await _unitofwork.employeerepo.getall();
                return View(employees);
                

            }
            else
            {
               
                
                    var employee = await _unitofwork.employeerepo.getbyemail(email);
                    return View(new List<employee> { employee });
               
               
                
                
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> create()
        {
             var departments= await _unitofwork.departmentrepo.getall();
            ViewData["departments"] = departments;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> create(createemployee cdto)
        {
            if (ModelState.IsValid)
            {
                var existingEmployee = await _unitofwork.employeerepo.getbyemail(cdto.email);
                if (existingEmployee != null)  // ❌ Email already exists
                {
                    ModelState.AddModelError("Email", "Email is already exist");
                    ViewData["departments"] = await _unitofwork.departmentrepo.getall(); 
                    return View(cdto);
                }
                if(cdto.image is not null) 
                { 
                cdto.imagename= documentsetting.uploadpath(cdto.image, "images");
                }

              var employee= _mapper.Map<employee>(cdto); // 🔧 Use AutoMapper to map properties

                await _unitofwork.employeerepo.add(employee);
                int count = await _unitofwork.complete();
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }

            ViewData["departments"] = await _unitofwork.departmentrepo.getall(); // 🔧 Also here for any other validation error
            return View(cdto);
        }


        public async Task<IActionResult> edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var employee = await _unitofwork.employeerepo.Get(id.Value);

            if (employee == null)
            {
                return NotFound();
            }

            var editdto = _mapper.Map<editdtoemployee>(employee); // 🔧 Use AutoMapper to map properties

            var departments = await _unitofwork.departmentrepo.getall();
            ViewData["departments"] = departments;


            return View(editdto);

        }
        [HttpPost]
        public async Task<IActionResult> edit([FromRoute] int? id, editdtoemployee edto)
        {
            if (id == null)
            {
                return BadRequest("Employee ID is required.");
            }

            if (ModelState.IsValid)
            {
                var employee = await _unitofwork.employeerepo.Get(id.Value);
                if (employee == null)
                {
                    return NotFound();
                }
                if (edto.imagename is not null&&edto.image is not null)
                {
                    documentsetting.delete(employee.imagename, "images");
                   
                }
                if (edto.image is not null)
                {
                    edto.imagename = documentsetting.uploadpath(edto.image, "images");
                }

                // Check if another employee already has the same email
                var existingEmployee = await _unitofwork.employeerepo.getbyemail(edto.email);
                if (existingEmployee != null && existingEmployee.id != id.Value)
                {
                    ViewData["departments"] = _unitofwork.departmentrepo.getall();
                    ModelState.AddModelError("Email", "Email is already in use by another employee.");
                    return View(edto);
                }

                // Update the employee (dont use _mapper.Map<employee>(edto); bc this will create a new instance of employee)
                _mapper.Map(edto, employee); //Takes an existing employee object (usually one already tracked by EF) and update it with edto

                 _unitofwork.employeerepo.update(employee);
                var count = await _unitofwork.complete();

                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewData["departments"] = _unitofwork.departmentrepo.getall();
            return View(edto);
        }

        public async Task<IActionResult> details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _unitofwork.employeerepo.Get(id.Value);
           var detailsdto = _mapper.Map<detailsemployeedto>(employee); // 🔧 Use AutoMapper to map properties

            ViewData["departments"] = await _unitofwork.departmentrepo.getall();
            return View(detailsdto);
        }
        public async Task<IActionResult> delete(int? id)    
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _unitofwork.employeerepo.Get(id.Value);
             _unitofwork.employeerepo.delete(employee);
            var count =await _unitofwork.complete();
            if (count > 0)
            {
                TempData["message"] = "emloyee has been deleted";
                return RedirectToAction("Index");
               
            }
            
            return BadRequest();
        }
       
       
        
    }
}
