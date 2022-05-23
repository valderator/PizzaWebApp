using Microsoft.EntityFrameworkCore;
using PizzaAPI.DBContext;
using PizzaAPI.Models;
using PizzaAPI.Repositories;
using PizzaAPI.Repositories.Interfaces;
using PizzaAPI.Services;
using PizzaAPI.Services.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPizzaService, PizzaService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IMailService, MailService>();

builder.Services.AddDbContext<PizzaDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PizzaSQL")));

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
    options.WithOrigins("http://localhost:3000").
    SetIsOriginAllowed(origin => true).
    AllowAnyMethod().
    AllowAnyHeader().
    AllowCredentials()
);

app.UseHttpsRedirection();
app.UseSession();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
