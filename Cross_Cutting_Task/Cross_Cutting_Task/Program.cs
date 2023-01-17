using Cross_Cutting_Task.Contexts;
using Cross_Cutting_Task.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Components.WebAssembly.Server;
using static AngouriMath.MathS;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});
builder.Services.AddCors(options => {
    options.AddPolicy("corspolicy", policy => {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
builder.Services.AddDbContext<FileItemContext>
(
    o => o.UseSqlServer(ConfigurationExtensions.GetConnectionString(builder.Configuration, "ProductConnection")));
builder.Services.AddScoped<IFileItemRepository, FileItemRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service_API", Version = "v1" });
}); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();
app.UseCors("corspolicy");
app.Run();
public partial class Program { }