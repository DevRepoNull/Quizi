using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Questioning_Data_Repositories.Context;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.Repositories.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Add Pattern On .Net

builder.Services.AddRazorPages(options =>
{
    //All folder and subfolder need be authorization
    //options.Conventions.AuthorizeFolder("/");

    //options.Conventions.AllowAnonymousToPage("/Login");
    //options.Conventions.AllowAnonymousToPage("/SignIn");
});

#endregion

#region Question DataBase Context Dependency

builder.Services.AddDbContext<WebContext>(option =>
    option.UseSqlServer(builder.Configuration.GetConnectionString("QuestiondbConnection"), sqlServerOptionsAction:
        sqlOption =>
        {
            sqlOption.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }));

#endregion

#region App Interface Dependency LifeTimes

builder.Services.AddScoped<IUserInterfaces, UserServices>();

builder.Services.AddScoped<IGeneralInterfaces, GeneralServices>();

builder.Services.AddScoped<IQuestionAndAnswerInterfaces, QuestionAndAnswerServices>();

builder.Services.AddScoped<IRoleInterfaces, RoleServices>();

builder.Services.AddScoped<IFormInterfaces, FormServices>();

#endregion

#region Question Exam Cookie

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(option =>
{
    const string loginPath = "/Login";
    const string logOutPath = "/LogOut";
    option.LoginPath = loginPath;
    option.LogoutPath = logOutPath;
    option.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
    option.Cookie.HttpOnly = true;
    option.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    option.Cookie.IsEssential = true;
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

#region Status Code

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode.Equals(404))
        context.Response.Redirect("/ErrorPages/Error404");
    else if (context.Response.StatusCode.Equals(500))
        context.Response.Redirect("/ErrorPages/Error500");
});

#endregion

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
