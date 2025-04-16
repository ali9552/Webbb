using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using website.dal.NewFolder;
using website.pl.dtos.roledto;
using website.pl.dtos.usersdto;

namespace website.pl.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<appuser> _userManager;
        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<appuser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string? rolename)
        {
            IEnumerable<roleviewdto> roles;
            if (string.IsNullOrEmpty(rolename))
            {

                roles = _roleManager.Roles.Select(x => new roleviewdto()
                {
                    id = x.Id,
                    name = x.Name,

                });


                return View(roles);

            }
            else
            {
                roles = _roleManager.Roles.Select(x => new roleviewdto()
                {
                    id = x.Id,
                    name = x.Name,

                }).Where(x => x.name.ToLower().Contains(rolename.ToLower()));
                return View(roles);
            }





        }
        [HttpGet]
        public IActionResult create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> create(roleviewdto roleviewdto)
        {
            if (ModelState.IsValid)
            {

                if (await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == roleviewdto.name) is not null)
                {
                    ModelState.AddModelError("name", "Role already exists.");
                    return View(roleviewdto);
                }

                var role = new IdentityRole()
                {
                    Name = roleviewdto.name,

                };
                var result = _roleManager.CreateAsync(role).Result;
                if (result.Succeeded)
                {
                    TempData["success"] = "Role created successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }
            return View(roleviewdto);

        }
        [HttpGet]
        public IActionResult details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            if (role == null) { return NotFound(); }


            var roledto = new roleviewdto();
            roledto.id = id;
            roledto.name = role.Name;

            return View(roledto);
        }

        public async Task<IActionResult> delete(string? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var user = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _roleManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
                TempData["success"] = "User deleted successfully";
            }
            else
            {
                ModelState.AddModelError("", "Failed to delete user");
                return View("Index");
            }


        }
        [HttpGet]
        public IActionResult edit([FromRoute] string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == id);
            if (role == null) { return NotFound(); }
            else
            {
                var roledto = new roleviewdto();
                roledto.id = id;
                roledto.name = role.Name;

                return View(roledto);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string? id, roleviewdto roleviewdto)
        {
            if (id == null || roleviewdto == null || id != roleviewdto.id)
            {
                ModelState.AddModelError("", "Invalid user ID.");
                return View(roleviewdto);
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }


            var existingrole = await _roleManager.Roles.FirstOrDefaultAsync(u => u.Name == roleviewdto.name && u.Id != id);


            if (existingrole != null)
            {
                ModelState.AddModelError("username", "Username already exists.");
            }




            role.Name = roleviewdto.name;
            role.Id = roleviewdto.id;


            var updateResult = await _roleManager.UpdateAsync(role);
            if (updateResult.Succeeded)
            {
                TempData["success"] = "User updated successfully.";
                return RedirectToAction("Index");
            }

            foreach (var error in updateResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(role);
        }

        [HttpGet]
        public async Task<IActionResult> addorremoveuser(string? id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            var usersiinrole = new List<userinroledto>();
            var users = _userManager.Users.ToList();
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound("Role not found.");
            if (users == null) return NotFound("Users not found.");

            foreach (var user in users)
            {
               
                var userinroledto = new userinroledto()
                {
                    userid = user.Id,
                    username = user.UserName,

                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {

                    userinroledto.isselected = true;
                }
                else
                {
                    userinroledto.isselected = false;

                }
                usersiinrole.Add(userinroledto);
            }
            ViewData["RoleId"] = id;
            return View(usersiinrole);

        }
        [HttpPost]
        public async Task<IActionResult> addorremoveuser(string? id, List<userinroledto> allusers)
        {
            if (ModelState.IsValid) 
            
            { 
                if(allusers is null) { return NotFound(); }
                var role = await _roleManager.FindByIdAsync(id);
                if (role == null) return NotFound("Role not found.");


                foreach (var user in allusers)
                {
                    var appuser = await _userManager.FindByIdAsync(user.userid);

                    if (user.isselected && !await _userManager.IsInRoleAsync(appuser,role.Name))
                    {
                       await _userManager.AddToRoleAsync(appuser,role.Name);

                    }
                    else if (!user.isselected && await _userManager.IsInRoleAsync(appuser, role.Name))
                    {
                        await _userManager.RemoveFromRoleAsync(appuser, role.Name);
                    }


                }
                TempData["success"] = "Users updated successfully.";
                return RedirectToAction("Index");


            }
            return View(allusers);


        }
    }
}
