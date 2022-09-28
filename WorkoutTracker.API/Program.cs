using Microsoft.EntityFrameworkCore;
using WorkoutTracker.API.Data;
using WorkoutTracker.API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
