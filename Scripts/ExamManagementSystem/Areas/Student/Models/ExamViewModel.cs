using System;
using EntityLayer.Entities;

namespace ExamManagementSystem.Areas.Student.Models
{
    public class ExamViewModel
    {
        public Exam Exam { get; set; }
        public bool HasTakenExam { get; set; }
    }

}

