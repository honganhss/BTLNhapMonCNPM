﻿using BTLNhapMonCNPM.Areas.Admin.Interfaces;
using BTLNhapMonCNPM.Areas.Admin.Repository;
using BTLNhapMonCNPM.Interface;
using BTLNhapMonCNPM.Models;
using BTLNhapMonCNPM.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<SanPhamIT, SanPhamImpl>();
builder.Services.AddScoped<LoaiSanPhamIT, LoaiSanPhamImpl>();
builder.Services.AddScoped<NhaCCIT, NhaCCImpl>();
builder.Services.AddScoped<CustomerAccountIT, CustomerAccountImpl>();
builder.Services.AddScoped<EmployeeAccountIT, EmployeeAccountImpl>();

// Add DbContext
builder.Services.AddDbContext<PharmacyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; 
});

builder.Services.AddHttpContextAccessor();
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
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{controller=Admin}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
