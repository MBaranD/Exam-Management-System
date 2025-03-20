using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Services.Abstract;
using BusinessLayer.Services.Concrete;
using EntityLayer.Entities;
using ExamManagementSystem.Areas.Student.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ExamManagementSystem.Areas.Student.Controllers
{
    public class StudentController : Controller
    {
        private readonly IResultService resultService;
        private readonly IExamService examService;
        private readonly IStudentService studentService;

        public StudentController(IResultService resultService, IExamService examService, IStudentService studentService)
        {
            this.resultService = resultService;
            this.examService = examService;
            this.studentService = studentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetStudentResult()
        {
            var results = await resultService.GetStudentResults();
            var model = new List<StudentResultViewModel>(); 
            foreach (var result in results)
            {
                var student = await studentService.GetStudentById(result.StudentId);
                var exam = await examService.GetExamById(result.ExamId);

                var viewModel = new StudentResultViewModel
                {
                    StudentName = student.NameSurname,
                    ExamName = exam.Name, 
                    Score = result.Score
                };

                model.Add(viewModel);
            }

            return View(model);
        }

    }
}
