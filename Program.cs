using Microsoft.EntityFrameworkCore;
using ProjetoParaProjetos.context;
using ProjetoParaProjetos.Interfaces;
using ProjetoParaProjetos.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//registrar BancoDadosRepositorio e IBancoDados
builder.Services.AddScoped<IBancoDados, BancoDadosRepositorio>();


string mySqlConnectionStr = builder.Configuration.GetConnectionString("DefaultConnection2");

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql (mySqlConnectionStr, ServerVersion. AutoDetect (mySqlConnectionStr)));

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

