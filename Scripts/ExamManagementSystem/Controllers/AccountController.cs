using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLayer.Services.Abstract;
using BusinessLayer.Services.Concrete;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ExamManagementSystem.Controllers
{
    public class AccountController : Controller
    {
    private readonly IStudentService studentService;
        private readonly IUserService _userService;

        public AccountController(IUserService userService, IStudentService studentService)
        {
            _userService = userService;
            this.studentService = studentService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (await _userService.LoginAsync(user.Email, user.Password))
            {
                var userEmail = await _userService.GetUserByEmail(user.Email);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, userEmail.Id.ToString()),
                    new Claim(ClaimTypes.Email, userEmail.Email),
                };

                var identity = new ClaimsIdentity(claims, "login");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("ExamProject", principal);
                if (userEmail.Role == "ADMIN")
                    return RedirectToAction("StudentList", "Student", new { area = "Admin" });
                if (userEmail.Role == "TEACHER")
                    return RedirectToAction("TeacherExamList", "Exam", new { area = "Teacher" });
                if (userEmail.Role == "STUDENT")
                    return RedirectToAction("StudentExamList", "Exam", new { area = "Student" });

                return RedirectToAction("ArticleList", "Article", new { area = "User" });

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Geçersiz e-posta veya şifre.");
                return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User registerDto)
        {
            var result = await studentService.RegisterAsync(registerDto);

            if (result)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }

            return RedirectToAction("Register", "Account", new { area = "" });
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("ExamProject");
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}