using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLayer.Services.Abstract;
using BusinessLayer.Services.Concrete;
using EntityLayer.Entities;
using ExamManagementSystem.Areas.Student.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;

namespace ExamManagementSystem.Areas.Student.Controllers
{
    public class ExamController : Controller
    {
        private readonly IExamService _examService;
        private readonly IResultService resultService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClaimsPrincipal _user;
        private readonly IToastNotification toastNotification;

        public ExamController(IExamService examService, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment, IResultService resultService, IHttpContextAccessor httpContextAccessor)
        {
            _examService = examService;
            this.toastNotification = toastNotification;
            this.resultService = resultService;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
        }
        [HttpGet]
        public async Task<IActionResult> StudentExamList()
        {
            var studentIdClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (studentIdClaim != null && int.TryParse(studentIdClaim.Value, out int studentId))
            {
                var results = await resultService.GetStudentResults();
                var exams = await _examService.GetAllExams();

                // Create a dictionary to store the results for each exam
                var examResults = results.ToDictionary(r => r.ExamId);

                // Create a view model to pass to the view
                var viewModel = exams.Select(exam => new ExamViewModel
                {
                    Exam = exam,
                    HasTakenExam = examResults.ContainsKey(exam.Id)
                }).ToList();

                return View(viewModel);
            }

            // Handle the case where the student ID couldn't be retrieved or parsed.
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> StartExam(int examId)
        {
            var exam = await _examService.GetExamById(examId);
            var questions = exam.Questions.ToList(); // HashSet<Question>'ı List<Question> tipine çevir

            ViewBag.ExamId = examId; // examId'yi ViewBag'e ekleyin

            return View(questions);
        }


        [HttpPost]
        public async Task<IActionResult> SubmitExam(List<string> answers, int examId)
        {
            var userIdClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                int score = 0;

                var questionAnswers = await _examService.GetExamById(examId);

                var questionsList = questionAnswers.Questions.ToList();

                for (int i = 0; i < questionsList.Count; i++)
                {
                    if (questionsList[i].CorrectAnswer == answers[i])
                    {
                        score += 10;
                    }
                }

                var result = new Result
                {
                    ExamId = examId,
                    StudentId = userId,
                    Score = score
                };

                await resultService.ResultAdd(result);

                TempData["SuccessMessage"] = "Cevaplar alındı ve puanlama yapıldı!";

                return RedirectToAction("StudentExamList", "Exam", new { area = "Student" });
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}