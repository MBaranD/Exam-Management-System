using EntityLayer.Entities;

namespace BusinessLayer.Services.Abstract
{
    public interface IUserService
    {
        Task ActiveStudent(int StudentId);
        Task PassiveStudent(int StudentId);
        Task DeleteStudent(int StudentId);
        Task StudentAdd(User user);
        Task<List<User>> GetAllStudents();
        Task<bool> LoginAsync(string email, string password);
        Task<User> GetUserByEmail(string email);
        Task<bool> RegisterAsync(User userRegister);
    }
}
