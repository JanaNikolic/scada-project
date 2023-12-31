using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using SCADA.Data;
using SCADA.Hubs;
using SCADA.Repository;
using SCADA.Repository.IRepository;
using SCADA.Service;
using SCADA.Service.IService;
using SCADA.Util;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("MyDbContextConnection");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IAlarmRepository, AlarmRepository>();
builder.Services.AddScoped<ITagRecordRepository, TagRecordRepository>();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAlarmService, AlarmService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IReportService, ReportService>();


// builder.Services.AddTransient<ShutdownConfig>();
builder.Services.AddHostedService<ShutdownConfig>();

// builder.Services.AddHostedService(provider => provider.GetRequiredService<ShutdownConfig>());
builder.Services.AddSignalR();

builder.Services.AddHostedService<SimulationService>();
builder.Services.AddHostedService<RTU>();

var app = builder.Build();

// var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
//
// using (var scope = scopedFactory.CreateScope())
// {
//     var service = scope.ServiceProvider.GetService<XMLConfig>();
//     service.XMLDeserialization();
// }
//
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");
app.UseWebSockets();

app.MapHub<RTUHubClient>("/hubs/rtu");
app.MapHub<SimulationHubClient>("/hubs/simulation");

app.Run();