using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Services.Abstract;
using BusinessLayer.Services.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace ExamManagementSystem.Areas.Admin.Controllers
{
    public class MessagesController : Controller
    {
        private readonly IMessageService messageService;

        public MessagesController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> InBox()
        {
            var result = await messageService.GetAllMessages();
            return View(result);
        }
    }
}