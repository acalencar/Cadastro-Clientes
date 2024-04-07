using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

//informando o db context usando opt => para indicar o tipo de banco que iremos usar
builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("DataBase"));

builder.Services.AddScoped<DataContext, DataContext>();



var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();


