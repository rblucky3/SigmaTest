using Sigma.Data;
using Microsoft.EntityFrameworkCore;
using Sigma.Service.Settings.Services;
using Sigma.Service.Settings.Interfaces;
using Sigma.Repository.Settings.Interfaces;
using Sigma.Repository.Settings.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



// Register repository and service
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<ICandidateService, CandidateService>();


// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
