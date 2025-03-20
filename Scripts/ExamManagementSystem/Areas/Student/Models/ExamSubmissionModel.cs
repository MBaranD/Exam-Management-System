using System;
namespace ExamManagementSystem.Areas.Student.Models
{
	public class ExamSubmissionModel
	{
        public int StudentId { get; set; }
        public int[] Answers { get; set; }
    }
}

