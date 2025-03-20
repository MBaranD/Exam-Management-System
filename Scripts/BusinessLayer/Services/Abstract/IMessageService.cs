using System;
using DataLayer.UnitOfWorks;
using EntityLayer.Entities;

namespace BusinessLayer.Services.Abstract
{
	public interface IMessageService
	{
        Task MessageAdd(Message message);
        Task<List<Message>> GetAllMessages();
    }
}

