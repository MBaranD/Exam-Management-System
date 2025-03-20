using System;
using System.Collections.Generic;
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
    public class TeacherController : Controller
    {
        private readonly ITeacherService teacherService;
        private readonly IToastNotification toastNotification;
        private readonly IValidator<User> validator;

        public TeacherController(ITeacherService teacherService, IToastNotification toastNotification, IValidator<User> validator)
        {
            this.teacherService = teacherService;
            this.toastNotification = toastNotification;
            this.validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> TeacherList()
        {
            var result = await teacherService.GetAllTeacher();
            return View(result);
        }
        public async Task<IActionResult> ActiveTeacher(int TeacherId)
        {
            await teacherService.ActiveTeacher(TeacherId);
            toastNotification.AddSuccessToastMessage("Öğretmen Onaylanmıştır.", new ToastrOptions { Title = "İşlem Başarılı" });
            return RedirectToAction("TeacherList", "Teacher");
        }
        public async Task<IActionResult> PassiveTeacher(int TeacherId)
        {
            await teacherService.PassiveTeacher(TeacherId);
            toastNotification.AddInfoToastMessage("Öğretmen Pasife Alınmıştır.", new ToastrOptions { Title = "İşlem Başarılı" });
            return RedirectToAction("TeacherList", "Teacher");
        }
        public async Task<IActionResult> DeleteTeacher(int TeacherId)
        {
            await teacherService.DeleteTeacher(TeacherId);
            toastNotification.AddErrorToastMessage("Öğretmen Silinmiştir.", new ToastrOptions { Title = "İşlem Başarılı" });
            return RedirectToAction("TeacherList", "Teacher");
        }
        [HttpGet]
        public async Task<IActionResult> TeacherAdd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TeacherAdd(User user)
        {
            var result = await validator.ValidateAsync(user);

            if (result.IsValid)
            {
                await teacherService.TeacherAdd(user);

                toastNotification.AddSuccessToastMessage("Öğretmen başarıyla eklenmiştir.", new ToastrOptions { Title = "İşlem Başarılı" });

                return RedirectToAction("TeacherList", "Teacher", new { Area = "Admin" });
            }
            result.AddToModelState(this.ModelState);
            return View();
        }
    }
}