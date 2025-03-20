using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Services.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace ExamManagementSystem.Areas.Teacher.Controllers
{
    public class ExamController : Controller
    {
        private readonly IExamService _examService;
        private readonly IToastNotification toastNotification;

        public ExamController(IExamService examService, IToastNotification toastNotification)
        {
            _examService = examService;
            this.toastNotification = toastNotification;
        }
        [HttpGet]
        public async Task<IActionResult> TeacherExamList()
        {
            var result = await _examService.GetAllExams();
            return View(result);
        }
        [HttpGet]
        public IActionResult TeacherExamAdd()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> TeacherExamAdd(Exam exam)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _examService.AddExam(exam);

                    toastNotification.AddInfoToastMessage("Sınav Başarıyla Eklenmiştir.", new ToastrOptions { Title = "İşlem Başarılı" });

                    return RedirectToAction("TeacherExamList", "Exam", new { Area = "Teacher" });
                }

                catch
                {
                    ModelState.AddModelError(string.Empty, "Sınav eklenirken bir hata oluştu. Lütfen tekrar deneyin.");
                }
            }

            return View(exam);
        }
    }
}