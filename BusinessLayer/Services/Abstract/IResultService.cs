using System;
using EntityLayer.Entities;

namespace BusinessLayer.Services.Abstract
{
	public interface IResultService
	{
        Task<List<Result>> GetAllResults();
        Task ResultAdd(Result result);
        Task<List<Result>> GetStudentResults();
        Task<List<Result>> GetAllResultsWithDetails();  }
}

