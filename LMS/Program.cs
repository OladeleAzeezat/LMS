using LMSData;
using LMSData.Models;
using LMSServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();
builder.Services.AddSingleton(builder.Configuration);
builder.Services.AddScoped<ILibraryAsset, LibraryAssetService>();
builder.Services.AddDbContext<LibraryContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryConnection")));


var app = builder.Build();      


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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
