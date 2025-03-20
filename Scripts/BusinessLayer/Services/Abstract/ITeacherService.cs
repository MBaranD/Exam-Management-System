using System;
using EntityLayer.Entities;

namespace BusinessLayer.Services.Abstract
{
	public interface ITeacherService
	{
        Task ActiveTeacher(int TeacherId);
        Task PassiveTeacher(int TeacherId);
        Task DeleteTeacher(int TeacherId);
        Task TeacherAdd(User user);
        Task<List<User>> GetAllTeacher();
    }
}

