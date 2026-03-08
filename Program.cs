using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(options =>
{
   options.UseSqlite(builder.Configuration.GetConnectionString("StoreDbConnection"),
   b => b.MigrationsAssembly("StoreApp.Web")); 
});

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
