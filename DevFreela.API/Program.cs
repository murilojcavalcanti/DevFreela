using DevFreela.API.Models.user;
using DevFreela.API.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string conString = builder.Configuration.GetConnectionString("DevfreelaCs");
builder.Services.AddDbContext<DevFreelaDbContext>(opts=>opts.UseSqlServer(conString));
//Configuranco o uso pelo appsettings
builder.Services.Configure<FreelanceTotalCostConfig>(builder.Configuration.GetSection("FreelanceTotalCostConfig"));
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

app.MapControllers();

app.Run();
