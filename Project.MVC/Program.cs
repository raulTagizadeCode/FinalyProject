using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.BL.Abstractions;
using Project.BL.Profiles.FoodProfiles;
using Project.BL.Services.abstractions;
using Project.DAL.Contexts;
using Project.DAL.Models;
using Project.DAL.Repository.abstractions;
using Project.DAL.Repository.implemantantions;
using Project.BL.DTOs.AppUserDTOs;
using Project.BL.Services.implemantantions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();




builder.Services.AddAutoMapper(typeof(FoodProfile).Assembly);
builder.Services.AddScoped<IRepository<Category>, Repository<Category>>();
builder.Services.AddScoped<IRepository<Food>, Repository<Food>>();
builder.Services.AddScoped<IRepository<Job>, Repository<Job>>();
builder.Services.AddScoped<IRepository<Rayting>, Repository<Rayting>>();
builder.Services.AddScoped<IRepository<JobCategory>, Repository<JobCategory>>();
builder.Services.AddScoped<IRepository<Masa>, Repository<Masa>>();
builder.Services.AddScoped<IRepository<TableCategoryNumber>, Repository<TableCategoryNumber>>();
builder.Services.AddScoped<IRepository<TableCategoryPlace>, Repository<TableCategoryPlace>>();
builder.Services.AddScoped<IRepository<JobApplication>, Repository<JobApplication>>();
builder.Services.AddScoped<IRepository<Reservation>, Repository<Reservation>>();
builder.Services.AddScoped<IRepository<Order>, Repository<Order>>();
builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IJobCategoryService, JobCategoryService>();
builder.Services.AddScoped<IJobApplicationService, JobApplicationService>();
builder.Services.AddScoped<IRaytingService, RaytingService>();
builder.Services.AddScoped<IMasaService, MasaService>();
builder.Services.AddScoped<ITableCategoryNumberService, TableCategoryNumberService>();
builder.Services.AddScoped<ITableCategoryPlaceService, TableCategoryPlaceService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IStripeService, StripeService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddValidatorsFromAssemblyContaining<AppUserRegisterDto>();
builder.Services.AddFluentValidationAutoValidation(); // the same old MVC pipeline behavior
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequireUppercase = false;
    opt.Password.RequiredLength = 4;
}).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();


builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"));
    opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});


builder.Services.AddAuthentication()
        .AddCookie(options =>
        {
            options.AccessDeniedPath = "/Account/AccessDenied/";
        });

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    var lastReset = dbContext.Settings.FirstOrDefault(s => s.Key == "LastTableReset")?.Value;
    if (lastReset == null || DateTime.Parse(lastReset).Date < DateTime.Today)
    {
        var tables = dbContext.Masas.ToList();
        foreach (var table in tables)
        {
            // Yalnız tarixdən əvvəl reset olunmuş masaları yeniləyir
            if (table.IsActive == false) // yalnız aktiv olmayan masalar
            {
                table.IsActive = true;
            }
        }
        dbContext.SaveChanges();

        // Tarixi yenilə
        var setting = dbContext.Settings.FirstOrDefault(s => s.Key == "LastTableReset");
        if (setting == null)
        {
            dbContext.Settings.Add(new Setting { Key = "LastTableReset", Value = DateTime.Today.ToString() });
        }
        else
        {
            setting.Value = DateTime.Today.ToString();
        }
        dbContext.SaveChanges();
    }
}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();



app.UseAuthorization();

app.MapControllerRoute(
  name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
