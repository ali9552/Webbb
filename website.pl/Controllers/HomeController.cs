using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using website.dal.NewFolder;
using website.pl.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using website.dal.context;

namespace website.pl.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly websitecontext _context;

        public HomeController(ILogger<HomeController> logger, websitecontext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Get employee counts
            ViewBag.EmployeeCount = _context.employees.Count();
            ViewBag.ActiveEmployeeCount = _context.employees.Count(e => e.isactive && !e.isdeleted);

            // Get department count
            ViewBag.DepartmentCount = _context.departments.Count();

            // Get recent hires (employees hired in the last 30 days)
            DateTime thirtyDaysAgo = DateTime.Now.AddDays(-30);
            ViewBag.RecentHiresCount = _context.employees.Count(e => e.hiringdate >= thirtyDaysAgo);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
