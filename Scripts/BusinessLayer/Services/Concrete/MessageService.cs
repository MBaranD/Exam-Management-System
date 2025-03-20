using System;
using DataLayer.UnitOfWorks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using BusinessLayer.Services.Abstract;
using EntityLayer.Entities;

namespace BusinessLayer.Services.Concrete
{
	public class MessageService : IMessageService
	{
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClaimsPrincipal _user;

        public MessageService(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
        }

        public async Task<List<Message>> GetAllMessages()
        {
            var messages = await unitOfWork.GetRepository<Message>().GetAllAsync();

            return messages;
        }
        public async Task MessageAdd(Message message)
        {
            await unitOfWork.GetRepository<Message>().AddAsync(message);
            await unitOfWork.SaveAsync();
        }
    }
}

