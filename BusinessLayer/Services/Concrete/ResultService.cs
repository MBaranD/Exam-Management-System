using System;
using DataLayer.UnitOfWorks;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using BusinessLayer.Services.Abstract;
using DataLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Concrete
{
    public class ResultService : IResultService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly AppDbContext dbContext;

        public ResultService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, AppDbContext dbContext)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }
        public async Task<List<Result>> GetAllResultsWithDetails()
        {
            var results = await dbContext.Results
                .Include(r => r.Student)
                .Include(r => r.Exam)
                .ToListAsync();

            return results;
        }

        public async Task<List<Result>> GetAllResults()
        {
            var results = await unitOfWork.GetRepository<Result>().GetAllAsync();

            return results;
        }
        public async Task<List<Result>> GetStudentResults()
        {
            var userIdClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                var results = await unitOfWork.GetRepository<Result>().GetAllAsync(x => x.StudentId == userId);
                return results;
            }

            return new List<Result>();
        }
        public async Task ResultAdd(Result result)
        {
            await unitOfWork.GetRepository<Result>().AddAsync(result);
            await unitOfWork.SaveAsync();
        }
    }
}

