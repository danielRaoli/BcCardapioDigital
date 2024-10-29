using BcCardapioDigital.API.Application.Services;
using BcCardapioDigital.API.Domain.Repositories;
using BcCardapioDigital.API.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = "Host=dpg-cs6k5lrtq21c73du3sug-a;Port=5432;Database=dbcardapio_bqew;Username=dbcardapio_bqew_user;Password=IPhDK2DSeyEfNOtzdYRF6Z3elhe3TPUj;SSL Mode=Require;Trust Server Certificate=true;";
var externalConnection = "Host=dpg-cs6k5lrtq21c73du3sug-a.oregon-postgres.render.com;Port=5432;Database=dbcardapio_bqew;Username=dbcardapio_bqew_user;Password=IPhDK2DSeyEfNOtzdYRF6Z3elhe3TPUj;SSL Mode=Require;Trust Server Certificate=true;";

builder.Services.AddDbContext<AppDbContext>(opts => opts.UseNpgsql("Host=dpg-cs6k5lrtq21c73du3sug-a;Port=5432;Database=dbcardapio_bqew;Username=dbcardapio_bqew_user;Password=IPhDK2DSeyEfNOtzdYRF6Z3elhe3TPUj;SSL Mode=Require;Trust Server Certificate=true;"));

builder.Services.AddScoped<IRepositorioCategoria, RepositorioCategoria>();
builder.Services.AddScoped<IRepositorioProduto, RepositorioProduto>();
builder.Services.AddScoped<IRepositorioPedido, RepositorioPedido>();
builder.Services.AddScoped<IRepositorioRestaurante, RepositorioRestaurante>();
builder.Services.AddScoped<IRepositorioHorario, RepositorioHorario>();
builder.Services.AddScoped<IAuthRepositorio, AuthRepositorio>();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IServicoCategoria, ServicoCategoria>();
builder.Services.AddScoped<IServicoProduto, ServicoProduto>();
builder.Services.AddScoped<IServicoPedido, ServicoPedido>();
builder.Services.AddScoped<IImageService, CloudinaryService>();
builder.Services.AddScoped<IServicoHorario, ServicoHorario>();
builder.Services.AddScoped<IServicoRestaurante, ServicoRestaurante>();

builder.Services.AddScoped<DbSeed>();

builder.Services.AddMemoryCache();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    var key = builder.Configuration["TokenConfiguration:SecurityKey"];

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key!)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddCors(opts => opts.AddPolicy("AppCors", policy =>
{
    policy.WithOrigins("http://localhost:5173")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials();

}));



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AppCors");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbInitializer = services.GetRequiredService<DbSeed>();
        await dbInitializer.SeedAsync(); // Executa o método de seed
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao inicializar o banco de dados: {ex.Message}");
    }
}


app.Run();
