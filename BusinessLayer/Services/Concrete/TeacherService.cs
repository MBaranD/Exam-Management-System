using System;
using DataLayer.UnitOfWorks;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using BusinessLayer.Services.Abstract;

namespace BusinessLayer.Services.Concrete
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClaimsPrincipal _user;

        public TeacherService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
        }
        public async Task<List<User>> GetAllTeacher()
        {
            var users = await unitOfWork.GetRepository<User>().GetAllAsync(x => x.Role == "TEACHER");

            return users;
        }
        public async Task ActiveTeacher(int TeacherId)
        {
            var user = await unitOfWork.GetRepository<User>().GetByIdAsync(TeacherId);
            user.IsActive = true;
            await unitOfWork.GetRepository<User>().UpdateAsync(user);
            await unitOfWork.SaveAsync();
        }
        public async Task PassiveTeacher(int TeacherId)
        {
            var user = await unitOfWork.GetRepository<User>().GetByIdAsync(TeacherId);
            user.IsActive = false;
            await unitOfWork.GetRepository<User>().UpdateAsync(user);
            await unitOfWork.SaveAsync();
        }
        public async Task DeleteTeacher(int TeacherId)
        {
            var user = await unitOfWork.GetRepository<User>().GetByIdAsync(TeacherId);

            await unitOfWork.GetRepository<User>().DeleteAsync(user);
            await unitOfWork.SaveAsync();
        }
        public async Task TeacherAdd(User user)
        {
            user.Role = "TEACHER";
            await unitOfWork.GetRepository<User>().AddAsync(user);
            await unitOfWork.SaveAsync();
        }
    }
}

