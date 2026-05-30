using Microsoft.EntityFrameworkCore;
using BlazorAcademyHWApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервис Razor Pages
builder.Services.AddRazorPages();

// Регистрируем контекст базы данных
builder.Services.AddDbContext<BlazorAcademyHWContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();