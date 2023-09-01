using BlazorLearning.Data.Data;
using BlazorLearning.Data.Repository;
using BlazorLearning.Data.Repository.Interface;
using BlazorLearning.Server.Interfaces;
using BlazorLearning.Server.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Donot forgot to add ConnectionStrings as "DefaultConnection" to the appsetting.json file
builder.Services.AddDbContext<DatabaseContext>
    (options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IEmployeeManager, EmployeeManager>();
builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();
//Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Error)
    .Enrich.FromLogContext()
    .WriteTo.File("../Logs/log.txt", rollingInterval: RollingInterval.Day)
    //.WriteTo.File(path: "Logs/log.txt", rollingInterval: RollingInterval.Day, outputTemplate:"{Timestamp: yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message}{NewLine}{Exception}")
    .CreateLogger();

//Use Serilog
builder.Logging.ClearProviders();
builder.Logging.AddSerilog();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V2");
});

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
