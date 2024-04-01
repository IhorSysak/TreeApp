using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TreeApplication.Context;
using TreeApplication.Extensions;
using TreeApplication.Interfaces;
using TreeApplication.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TreeContext>(option =>
    option.UseNpgsql(builder.Configuration.GetConnectionString("TreeContext")));

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ITreeService, TreeService>();
builder.Services.AddScoped<INodeService, NodeService>();
builder.Services.AddScoped<IJournalService, JournalService>();

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
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

app.ConfigureCustomExeptionMiddleware();

app.UseAuthorization();
app.MapControllers();

app.Run();
