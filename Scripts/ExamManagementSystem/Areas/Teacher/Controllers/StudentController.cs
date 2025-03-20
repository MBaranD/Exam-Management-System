using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Services.Abstract;
using BusinessLayer.Services.Concrete;
using EntityLayer.Entities;
using ExamManagementSystem.Areas.Student.Models;
using ExamManagementSystem.Areas.Teacher.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ExamManagementSystem.Areas.Teacher.Controllers
{
    public class StudentController : Controller
    {
        private readonly IResultService resultService;
        private readonly IExamService examService;
        private readonly IStudentService studentService;
        public StudentController(IResultService resultService, IStudentService studentService, IExamService examService)
        {
            this.resultService = resultService;
            this.studentService = studentService;
            this.examService = examService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentsResults()
        {
            var results = await resultService.GetAllResultsWithDetails();
            return View(results);
        }
    }
}
