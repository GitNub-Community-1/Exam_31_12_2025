using Domain.Models.Entity;
using Infastructure.AutoMapper;
using Infastructure.Data;
using Infastructure.Services;
using Infastructure.Services.Interfaces;
using Infastructure.Workers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Quartz;
using WebAPIWithJWTAndIdentity.MiddleWare;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);


var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connection));


builder.Services.AddScoped<INotificationTypeService,NotificationTypeService>();
builder.Services.AddScoped<IUserNotificationService,UserNotificationService>();

builder.Services.AddMemoryCache();
builder.Services.AddHostedService<AutoExpiredAndAddedToReportLog>();


builder.Services.AddAutoMapper(typeof(MapperProfile));


builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    
    var jobKey = new JobKey("NotificationExpirationJob");
    q.AddJob<NotificationExpirationJob>(opts => opts.WithIdentity(jobKey));
    
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("NotificationExpirationTrigger")
        .WithCronSchedule("0 * * * * ?")); 
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);


// Настройка CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
    {
        builder.WithOrigins("http://localhost:3000", "http://localhost:5000", "https://localhost:5001")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Advertisement Platform API",
        Version = "v1",
        Description = "API for advertisements and categories"
    });
});

builder.Services.AddControllers();

var app = builder.Build();


if (!builder.Environment.IsEnvironment("EfMigration"))
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    var dbContext = services.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();
}


app.UseMiddleware<CustomLoggingMiddleware>();

// Использование CORS
app.UseCors("AllowLocalhost");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
