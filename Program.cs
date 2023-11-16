using Microsoft.EntityFrameworkCore;
using Prova.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDataContext>(
    options => options.UseSqlite("Data Source=Folha.db;")
);

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors
(
    c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
);


app.MapControllers();

app.Run();
