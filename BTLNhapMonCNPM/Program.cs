using BTLNhapMonCNPM.Interface;
using BTLNhapMonCNPM.Models;
using BTLNhapMonCNPM.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<SanPhamIT, SanPhamImpl>();
builder.Services.AddScoped<LoaiSanPhamIT, LoaiSanPhamImpl>();
builder.Services.AddScoped<NhaCCIT, NhaCCImpl>();
// Add DbContext

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
