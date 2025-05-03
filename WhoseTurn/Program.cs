using Microsoft.EntityFrameworkCore;
using WhoseTurn.Repositories;
using WhoseTurn.DatabaseContext;
using WhoseTurn.Managers;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("Default"));
var dataSource = dataSourceBuilder.Build();

builder.Services.AddDbContextPool<DeterminantContext>(opt =>
    opt.UseNpgsql(dataSource));

builder.Services
     .AddScoped<IDeterminantManager, DeterminantManager>()
    .AddScoped<IDeterminantRepository, DeterminantRepository>()
;

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

// 3WFO3z2bO6knMysC