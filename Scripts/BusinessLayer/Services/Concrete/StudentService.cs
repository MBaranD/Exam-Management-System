using System;
using DataLayer.UnitOfWorks;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using BusinessLayer.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Concrete
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClaimsPrincipal _user;

        public StudentService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
        }
        public async Task<User> GetStudentById(int studentId)
        {
            return await unitOfWork.GetRepository<User>().GetAsync(x=>x.Id == studentId);
        }
        public async Task<bool> LoginAsync(string email, string password)
        {
            var user = await unitOfWork.GetRepository<User>().GetAsync(u => u.Email == email && u.Password == password && u.IsActive);

            if (user != null)
            {
                return true;
            }

            return false;
        }
        public async Task<List<User>> GetAllStudents()
        {
            var users = await unitOfWork.GetRepository<User>().GetAllAsync(x => x.Role == "STUDENT");

            return users;
        }
        public async Task<bool> RegisterAsync(User user)
        {
            await unitOfWork.GetRepository<User>().AddAsync(user);
            await unitOfWork.SaveAsync();

            return true;
        }
        public async Task ActiveStudent(int StudentId)
        {
            var user = await unitOfWork.GetRepository<User>().GetByIdAsync(StudentId);
            user.IsActive = true;
            await unitOfWork.GetRepository<User>().UpdateAsync(user);
            await unitOfWork.SaveAsync();
        }
        public async Task PassiveStudent(int StudentId)
        {
            var user = await unitOfWork.GetRepository<User>().GetByIdAsync(StudentId);
            user.IsActive = false;
            await unitOfWork.GetRepository<User>().UpdateAsync(user);
            await unitOfWork.SaveAsync();
        }
        public async Task DeleteStudent(int StudentId)
        {
            var user = await unitOfWork.GetRepository<User>().GetByIdAsync(StudentId);

            await unitOfWork.GetRepository<User>().DeleteAsync(user);
            await unitOfWork.SaveAsync();
        }
        public async Task StudentAdd(User user)
        {
            await unitOfWork.GetRepository<User>().AddAsync(user);
            await unitOfWork.SaveAsync();
        }
    }
}

