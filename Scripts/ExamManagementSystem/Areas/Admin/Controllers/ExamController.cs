using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Services.Abstract;
using BusinessLayer.Services.Concrete;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace ExamManagementSystem.Areas.Admin.Controllers
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
        public async Task<IActionResult> ExamList()
        {
            var result = await _examService.GetAllExams();
            return View(result);
        }
        [HttpGet]
        public IActionResult ExamAdd()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ExamAdd(Exam exam)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _examService.AddExam(exam);

                    toastNotification.AddInfoToastMessage("Sınav Başarıyla Eklenmiştir.", new ToastrOptions { Title = "İşlem Başarılı" });

                    return RedirectToAction("ExamList", "Exam");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Sınav eklenirken bir hata oluştu. Lütfen tekrar deneyin.");
                }
            }

            return View(exam);
        }

        public async Task<IActionResult> ExamDelete(int examId)
        {
            await _examService.RemoveExam(examId);
            toastNotification.AddInfoToastMessage("Sınav Başarıyla Silinmiştir.", new ToastrOptions { Title = "İşlem Başarılı" });
            return RedirectToAction("ExamList", "Exam");
        }
    }
}