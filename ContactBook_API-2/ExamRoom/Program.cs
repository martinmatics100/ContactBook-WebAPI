using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ContactBook_API.Models;
using Microsoft.EntityFrameworkCore;
using ContactBook_API.Models.DbContext;
using ContactBook_API.Data.Models;
using ContactBook_API.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDependencies(builder.Configuration);

//
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<MartinDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//This will allow any origin, header, and HTTP method to access your API, which might be useful during development. For production, you should consider configuring CORS settings more restrictively.
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseHttpsRedirection();
 
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
