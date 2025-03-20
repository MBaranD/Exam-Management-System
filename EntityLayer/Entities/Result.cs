using System;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities
{
    public class Result
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public User Student { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public int Score { get; set; }
    }
}

