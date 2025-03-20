using System.Diagnostics;
using BusinessLayer.Services.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using ExamManagementSystem.Models;
using NToastNotify;
using BusinessLayer.Services.Concrete;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace ExamManagementSystem.Controllers;

public class HomeController : Controller
{
    private readonly IStudentService studentService;
    private readonly IUserService _userService;
    private readonly IMessageService messageService;

    public HomeController(IStudentService studentService, IMessageService messageService, IUserService userService)
    {
        this.studentService = studentService;
        this.messageService = messageService;
        _userService = userService;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult About()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Contact(Message message)
    {
        await messageService.MessageAdd(message);

        return RedirectToAction("Index", "Home", new { area = "" });
    }
}

