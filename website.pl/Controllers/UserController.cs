using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using website.dal.NewFolder;
using website.pl.dtos.usersdto;

namespace website.pl.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<appuser> _identityUser;
        public UserController(UserManager<appuser> identityUser)
        {
            _identityUser = identityUser;

        }
        [HttpGet]
        public async Task<IActionResult> Index(string? username)
        {
            IEnumerable<userdto> users;
            if (string.IsNullOrEmpty(username))
            {

                users = _identityUser.Users.Select(x => new userdto()
                {
                    id = x.Id,
                    username = x.UserName,
                    email = x.Email,
                    fname = x.firstname,
                    lname = x.lastname,
                    roles = _identityUser.GetRolesAsync(x).Result
                });


                return View(users);

            }
            else
            {
                users = _identityUser.Users.Select(x => new userdto()
                {
                    id = x.Id,
                    username = x.UserName,
                    email = x.Email,
                    fname = x.firstname,
                    lname = x.lastname,
                    roles = _identityUser.GetRolesAsync(x).Result
                }).Where(x => x.username.ToLower().Contains(username.ToLower()));
                return View(users);
            }





        }
        [HttpGet]
        public IActionResult details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = _identityUser.Users.FirstOrDefault(x => x.Id == id);
            if (user == null) { return NotFound(); }


            var userdto = new userdto();
            userdto.id = id;
            userdto.username = user.UserName;
            userdto.fname = user.firstname;
            userdto.lname = user.lastname;
            userdto.email = user.Email;
            userdto.roles = _identityUser.GetRolesAsync(user).Result;
            return View(userdto);
        }

        public async Task<IActionResult> delete(string? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var user = await _identityUser.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _identityUser.DeleteAsync(user);
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
            var user = _identityUser.Users.FirstOrDefault(x => x.Id == id);
            if (user == null) { return NotFound(); }
            else
            {
                var userdto = new userdto();
                userdto.id = id;
                userdto.username = user.UserName;
                userdto.fname = user.firstname;
                userdto.lname = user.lastname;
                userdto.email = user.Email;
                userdto.roles = _identityUser.GetRolesAsync(user).Result;
                return View(userdto);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string? id, userdto userdto)
        {
            if (id == null || userdto == null || id != userdto.id)
            {
                ModelState.AddModelError("", "Invalid user ID.");
                return View(userdto);
            }

            var user = await _identityUser.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            
            var existingUsername = await _identityUser.Users
                .FirstOrDefaultAsync(u => u.UserName == userdto.username && u.Id != id);

            if (existingUsername != null)
            {
                ModelState.AddModelError("username", "Username already exists.");
            }

           
            var existingEmail = await _identityUser.Users
                .FirstOrDefaultAsync(u => u.Email == userdto.email && u.Id != id);

            if (existingEmail != null)
            {
                ModelState.AddModelError("email", "Email already exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(userdto);
            }

            
            user.UserName = userdto.username;
            user.firstname = userdto.fname;
            user.lastname = userdto.lname;
            user.Email = userdto.email;

            var updateResult = await _identityUser.UpdateAsync(user);
            if (updateResult.Succeeded)
            {
                TempData["success"] = "User updated successfully.";
                return RedirectToAction("Index");
            }

            foreach (var error in updateResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(userdto);
        }






    }
    }
