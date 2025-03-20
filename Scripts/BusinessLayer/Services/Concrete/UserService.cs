using DataLayer.UnitOfWorks;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using BusinessLayer.Services.Abstract;

namespace BusinessLayer.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClaimsPrincipal _user;

        public UserService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
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
        public async Task<User> GetUserByEmail(string email)
        {
            return await unitOfWork.GetRepository<User>().GetAsync(u => u.Email == email);
        }
        public async Task<List<User>> GetAllStudents()
        {
            var users = await unitOfWork.GetRepository<User>().GetAllAsync(x => x.Role == "USER");

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

        public Task<List<User>> GetAllUsersForApprove()
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAuthorByIdAsync(Guid authorId)
        {
            throw new NotImplementedException();
        }
        public Task<User> GetUserProfileAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserRoleByUserId()
        {
            throw new NotImplementedException();
        }

        public Task PassiveAccount()
        {
            throw new NotImplementedException();
        }

        public Task PassiveUser(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserProfileAsync(User userProfileDto)
        {
            throw new NotImplementedException();
        }
    }
}
