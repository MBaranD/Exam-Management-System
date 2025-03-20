using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Services.Abstract;
using EntityLayer.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace ExamManagementSystem.Areas.Admin.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService studentService;
        private readonly IResultService resultService;
        private readonly IToastNotification toastNotification;
        private readonly IValidator<User> validator;

        public StudentController(IStudentService studentService, IToastNotification toastNotification, IValidator<User> validator, IResultService resultService)
        {
            this.studentService = studentService;
            this.toastNotification = toastNotification;
            this.validator = validator;
            this.resultService = resultService;
        }

        [HttpGet]
        public async Task<IActionResult> StudentList()
        {
            var result = await studentService.GetAllStudents();
            return View(result);
        }
        public async Task<IActionResult> ActiveStudent(int StudentId)
        {
            await studentService.ActiveStudent(StudentId);
            toastNotification.AddSuccessToastMessage("Öğrenci Onaylanmıştır.", new ToastrOptions { Title = "İşlem Başarılı" });
            return RedirectToAction("StudentList", "Student");
        }
        public async Task<IActionResult> PassiveStudent(int StudentId)
        {
            await studentService.PassiveStudent(StudentId);
            toastNotification.AddInfoToastMessage("Öğrenci Pasife Alınmıştır.", new ToastrOptions { Title = "İşlem Başarılı" });
            return RedirectToAction("StudentList", "Student");
        }
        public async Task<IActionResult> DeleteStudent(int StudentId)
        {
            await studentService.DeleteStudent(StudentId);
            toastNotification.AddErrorToastMessage("Öğrenci Silinmiştir.", new ToastrOptions { Title = "İşlem Başarılı" });
            return RedirectToAction("StudentList", "Student");
        }
        [HttpGet]
        public async Task<IActionResult> StudentAdd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StudentAdd(User user)
        {
            var result = await validator.ValidateAsync(user);

            if (result.IsValid)
            {
                await studentService.StudentAdd(user);

                toastNotification.AddSuccessToastMessage("Öğrenci başarıyla eklenmiştir.", new ToastrOptions { Title = "İşlem Başarılı" });

                return RedirectToAction("StudentList", "Student", new { Area = "Admin" });
            }
            result.AddToModelState(this.ModelState);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> StudentResult()
        {
            var result = await resultService.GetAllResultsWithDetails();
            return View(result);
        }
    }
}