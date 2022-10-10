using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.API.Data;
using WorkoutTracker.API.Repository;
using WorkoutTracker.Domain;
using WorkoutTracker.Domain.Extensions;
using Microsoft.Extensions.DependencyInjection;
using WorkoutTracker.Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
}));

var configDatabaseType = builder.Configuration.GetValue(typeof(string), "DatabaseType").ToString();

if (configDatabaseType == "Memory")
{
    builder.Services.AddTransient<IWorkoutRepository, InMemoryWorkoutRepository>();
}
else if (configDatabaseType == "SQL")
{
    builder.Services.AddDbContext<WorkoutDbContext>(
        o => o.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
    builder.Services.AddTransient<IWorkoutRepository, SqlWorkoutRepository>();

    builder.Services.AddIdentityServices(builder.Configuration);

    //builder.Services.AddTransient<User>();
    builder.Services.AddTransient<IAccountRepository, SqlAccountRepository>();
}
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
