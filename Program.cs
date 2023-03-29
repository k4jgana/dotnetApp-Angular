

using DutchTreat.Controllers;
using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using System.Reflection;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(cfg =>
    {
        cfg.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

builder.Services.AddSingleton(builder.Configuration);



builder.Services.AddIdentity<StoreUser, IdentityRole>(cfg=>cfg.User.RequireUniqueEmail=true)
    .AddEntityFrameworkStores<DutchContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication().AddCookie()
.AddJwtBearer(cfg => 
    {
        var config = builder.Configuration.GetSection("Tokens");
        cfg.TokenValidationParameters = new TokenValidationParameters() 
        {
            ValidIssuer = config["Issuer"],
            ValidAudience = config["Audience"],
            IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Key"]))

        };
    });


builder.Services.AddScoped<UserManager<StoreUser>>();
builder.Services.AddDbContext<DutchContext>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddTransient<IMailService, NullMailService>();
builder.Services.AddScoped<IDutchRepository, DutchRepository>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddTransient<DutchSeeder>();

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("config.json")
    .AddEnvironmentVariables();








var app = builder.Build();



void RunSeeding(WebApplication app)
{
    var scopeFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopeFactory.CreateScope())
    {
        var seeder = scope.ServiceProvider.GetService<DutchSeeder>();
        seeder.Seed();
        seeder.SeedAsync().Wait();
    }

}

app.UseDeveloperExceptionPage();

app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowAllOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action}/{id?}",
        defaults: new { controller = "App", action = "Shop" });
});

if (args.Length == 1 && args[0].ToLower() == "/seed")
{
    RunSeeding(app);
}
else
{
    app.Run();
}