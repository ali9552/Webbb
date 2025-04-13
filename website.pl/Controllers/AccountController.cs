using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using website.dal.NewFolder;

using website.pl.dtos.accountdto;
using website.pl.helpers;
using website.pl.Views.Account;


namespace website.pl.Controllers
{
    public class AccountController : Controller

    {
        private readonly SignInManager<appuser> _signInManager;
        private readonly UserManager<appuser> _userManager;
        private readonly IMapper _mapper;
        public AccountController(UserManager<appuser> model, IMapper mapper, SignInManager<appuser> signin)
        {
            _userManager = model;
            _mapper = mapper;
            _signInManager = signin;
        }
        // GET: /Account/Login  
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }
        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Signup(signupdto signupdto)
        {

            if (ModelState.IsValid)
            {

                if (signupdto.isagree == false)
                {
                    ModelState.AddModelError("", "You must agree to the terms and conditions.");
                    return View(signupdto);
                }

                var result1 = await _userManager.FindByEmailAsync(signupdto.email);
                if (result1 is not null)
                {
                    ModelState.AddModelError("", "Email already exists.");
                    return View(signupdto);
                }
                var result2 = await _userManager.FindByNameAsync(signupdto.username);
                if (result2 is not null)
                {
                    ModelState.AddModelError("", "Username already exists.");
                    return View(signupdto);
                }


                var user = _mapper.Map<appuser>(signupdto); var result = await _userManager.CreateAsync(user, signupdto.password);

                if (result.Succeeded)
                {
                    // You can add additional logic here, such as signing in the user or redirecting to a different page
                    return RedirectToAction("login");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }


            }
            return BadRequest("Invalid signup attempt.");

        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(logindto logindto)
        {
            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByEmailAsync(logindto.emailorusername);
                if (user == null)
                {
                    user = await _userManager.FindByNameAsync(logindto.emailorusername);
                }
                if (user is not null)
                {

                    var result1 = await _userManager.CheckPasswordAsync(user, logindto.password);
                    if (result1)
                    {

                        var result = await _signInManager.PasswordSignInAsync(user, logindto.password, false, lockoutOnFailure: false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid password.");
                        return View(logindto);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "user not found");
                    return View(logindto);
                }

            }
            return BadRequest("Invalid login attempt.");
        }

        public new async Task<IActionResult> signout()
        {

            await _signInManager.SignOutAsync();
            return RedirectToAction("login", "Account");
        }
        public IActionResult forgetpassword()
        {


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> sendemail(forgetpassworddto model)
        {
            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByEmailAsync(model.email);
                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var url = Url.Action("resetpassword", "Account", new { email = model.email, token }, Request.Scheme);
                    var email = new email() { to = model.email, subject = "reset password", body = url };
                    emailsetting.sendemail(email);
                    TempData["message"] = "Email sent successfully.";

                    return RedirectToAction("login", "Account");

                }
                ModelState.AddModelError("", "Email not found.");
                return View(model);
            }


            return BadRequest("there is an error accour");

        }

        [HttpGet]
        public async Task<IActionResult> resetpassword(string? email, string? token)
        {
            TempData["email"] = email;
            TempData["token"] = token;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> resetpassword( resetdto resetdto)
        {

            if (ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;
                if (email is null||token is null)
                {
                    ModelState.AddModelError("", "Email not found.");
                    return View();
                }
                var user = await _userManager.FindByEmailAsync(email);
                if (user is not null) 
                {
                    var res = await _userManager.ResetPasswordAsync(user, token, resetdto.password);

                    if (res.Succeeded)
                    {
                        TempData["message"] = "Password reset successfully.";
                        return RedirectToAction("login", "Account");
                    }
                    else
                    {
                        foreach (var error in res.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }

                }
               

            }
            return BadRequest("something went error");

        }
    }
}

