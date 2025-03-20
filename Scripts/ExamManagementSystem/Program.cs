using DataLayer.Extensions;
using ServiceLayer.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.LoadDataLayerExtension(builder.Configuration);
builder.Services.LoadServiceLayerExtension();

builder.Services.AddSession();
builder.Services.AddControllersWithViews()
    .AddNToastNotifyToastr(new NToastNotify.ToastrOptions()
    {
        TimeOut = 3000
    })
    .AddRazorRuntimeCompilation();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = new PathString("/Account/Login");
    config.LogoutPath = new PathString("/Admin/Auth/Logout");
    config.Cookie = new CookieBuilder
    {
        Name = "ExamProject",
        HttpOnly = true,
        SameSite = SameSiteMode.Strict,
        SecurePolicy = CookieSecurePolicy.SameAsRequest
    };
    config.SlidingExpiration = true;
    config.ExpireTimeSpan = TimeSpan.FromDays(1);
    config.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied");
});

builder.Services.AddAuthentication("ExamProject")
        .AddCookie("ExamProject", config =>
        {
            config.LoginPath = "/Account/Login";
            config.LogoutPath = "/Account/Logout";
        });
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseNToastNotify();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Admin",
    pattern: "/Admin/{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "Admin" }
);

app.MapControllerRoute(
    name: "Teacher",
    pattern: "/Teacher/{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "Teacher" }
);
app.MapControllerRoute(
    name: "Student",
    pattern: "/Student/{controller=Home}/{action=Index}/{id?}",
    defaults: new { area = "Student" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
app.Run();

