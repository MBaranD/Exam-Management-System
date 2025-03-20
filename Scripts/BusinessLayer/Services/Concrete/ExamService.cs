using System;
using BusinessLayer.Services.Abstract;
using DataLayer.Context;
using DataLayer.UnitOfWorks;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Concrete
{
    public class ExamService : IExamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext dbContext;

        public ExamService(IUnitOfWork unitOfWork, AppDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            this.dbContext = dbContext;
        }
        public async Task<Exam> GetExamById(int examId)
        {
            return await dbContext.Exams
                .Where(x => x.Id == examId)
                .Include(exam => exam.Questions)
                .FirstOrDefaultAsync();
        }
        public async Task AddExam(Exam exam)
        {
            await _unitOfWork.GetRepository<Exam>().AddAsync(exam);

            // Save all questions and choices
            foreach (var question in exam.Questions)
            {
                await _unitOfWork.GetRepository<Question>().AddAsync(question);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveExam(int examId)
        {
            var exam = await _unitOfWork.GetRepository<Exam>().GetByIdAsync(examId);

            if (exam != null)
            {
                // Remove associated questions
                var questions = dbContext.Questions.Where(q => q.ExamId == examId).ToList();
                foreach (var question in questions)
                {
                    dbContext.Questions.Remove(question);
                }

                await _unitOfWork.GetRepository<Exam>().DeleteAsync(exam);
                await _unitOfWork.SaveAsync();
            }
        }
        public async Task<List<Exam>> GetAllExams()
        {
            return await dbContext.Exams
                .Include(exam => exam.Questions)
                .ToListAsync();
        }
    }
}

