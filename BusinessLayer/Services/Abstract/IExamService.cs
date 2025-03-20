using System;
using EntityLayer.Entities;

namespace BusinessLayer.Services.Abstract
{
    public interface IExamService
    {
        Task AddExam(Exam exam);
        Task RemoveExam(int examId);
        Task<List<Exam>> GetAllExams();
        Task<Exam> GetExamById(int examId);
    }
}

