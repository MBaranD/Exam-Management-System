using ServiceLayer.FluentValidations;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using BusinessLayer.Services.Concrete;
using BusinessLayer.Services.Abstract;

namespace ServiceLayer.Extensions
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IExamService, ExamService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IResultService, ResultService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



            services.AddControllersWithViews().AddFluentValidation(opt =>
            {
                opt.RegisterValidatorsFromAssemblyContaining<UserValidator>();
                opt.DisableDataAnnotationsValidation = true;
                opt.ValidatorOptions.LanguageManager.Culture = new System.Globalization.CultureInfo("tr");
            });

            return services;
        }
    }
}
